#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.ReflectionModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using Caliburn.Micro;
using Gemini.Framework.Services;
using Gemini.Properties;
using Gu.Localization;
using NLog;

#endregion

namespace Gemini
{
    public class AppBootstrapper : BootstrapperBase
    {
        private List<Assembly> _priorityAssemblies;
        protected CompositionContainer Container { get; set; }
        internal IList<Assembly> PriorityAssemblies => _priorityAssemblies;

        readonly static Logger _Log = NLog.LogManager.GetCurrentClassLogger();
        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            _Log.Error($"\nCurrent_DispatcherUnhandledException: UNHANDLED_LOG_BEGIN");
            _Log.Error(e);
            _Log.Error(e.Exception);
            _Log.Error(e.Exception.StackTrace);
            _Log.Error($"\n\nCurrent_DispatcherUnhandledException: UNHANDLED_LOG_END");
        }

        public void UnhandledExceptionEventHandler(object sender, System.UnhandledExceptionEventArgs e)
        {
            // in case of unhandled exception - delete the user setting file, a workaround
            _Log.Error($"\n\nUnhandledExceptionEventHandler: CRASH_LOG_BEGIN");
            _Log.Error(e);
            _Log.Error(e.ExceptionObject);
            _Log.Error(e.ExceptionObject as Exception);

            _Log.Error($"Save this logs for analysis, terminating the app...");
            _Log.Error($"\n\nUnhandledExceptionEventHandler: CRASH_LOG_END");
        }


        public AppBootstrapper()
        {
            try
            {
                //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(this.LoadFromSameFolder);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionEventHandler);
                Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

                PreInitialize();
                Initialize();
            }
            catch (System.TypeLoadException tle)
            {
                _Log.Error(tle);
                System.IO.File.AppendAllLines("./LOGLOGLOG.txt", new[] { $"{tle?.TypeName} ---- {tle?.Message}" });
                System.IO.File.AppendAllLines("./LOGLOGLOG.txt", new[] { $"{tle?.InnerException}" });

                System.IO.File.AppendAllLines("c:\\logs\\LOGLOGLOG.txt", new[] { $"{tle?.TypeName} ---- {tle?.Message}" });
                System.IO.File.AppendAllLines("c:\\logs\\LOGLOGLOG.txt", new[] { $"{tle?.InnerException}" });

                _Log.Error(tle?.InnerException);
                throw;
            }
            catch(Exception e) {
                _Log.Error(e);
                System.IO.File.AppendAllLines("./LOGLOGLOG.txt", new[] { $"{e}" });
                System.IO.File.AppendAllLines("c:\\logs\\LOGLOGLOG.txt", new[] { $"{e}" });

                if (e is System.Reflection.ReflectionTypeLoadException)
                {
                    var typeLoadException = e as System.Reflection.ReflectionTypeLoadException;
                    var loaderExceptions = typeLoadException.LoaderExceptions;
                    foreach (var ee in loaderExceptions)
                    {
                        System.IO.File.AppendAllLines("./LOGLOGLOG.txt", new[] { $"{ee}" });
                        System.IO.File.AppendAllLines("c:\\logs\\LOGLOGLOG.txt", new[] { $"{ee}" });
                        _Log.Error(ee);
                    }
                }
                throw;
            }
        }

        public virtual void PreInitialize()
        {
            var code = Settings.Default.LanguageCode;

            if (!string.IsNullOrWhiteSpace(code))
                try
                {
                    var culture = CultureInfo.GetCultureInfo(code);
                    Translator.Culture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;
                    Thread.CurrentThread.CurrentCulture = culture;
                }
                catch
                {
                    // fallback to default
                }
        }
        public virtual string CatalogPath => @"./";

        protected virtual System.ComponentModel.Composition.Primitives.ComposablePartCatalog MakeCatalog() {
            // Make composable part catalog (using a DirectoryCatalog).
            return new DirectoryCatalog(CatalogPath);
        }

        /// <summary>
        ///     By default, we are configured to use MEF
        /// </summary>
        protected override void Configure()  {
            // Add all assemblies to AssemblySource.
            var catalog = MakeCatalog();
            
            AssemblySource.Instance.AddRange(
            catalog.Parts
                .Select(part => ReflectionModelServices.GetPartType(part).Value.Assembly)
                .Where(assembly => !AssemblySource.Instance.Contains(assembly)));

            // Prioritise the executable assembly. This allows the client project to override exports, including IShell.
            // The client project can override SelectAssemblies to choose which assemblies are prioritised.
            _priorityAssemblies = SelectAssemblies().ToList();
            var priorityCatalog = new AggregateCatalog(_priorityAssemblies.Select(x => new AssemblyCatalog(x)));
            var priorityProvider = new CatalogExportProvider(priorityCatalog);

            // Now get all other assemblies (excluding the priority assemblies).
            var mainCatalog = new AggregateCatalog(
                AssemblySource.Instance
                    .Where(assembly => !_priorityAssemblies.Contains(assembly))
                    .Select(x => new AssemblyCatalog(x)));
            var mainProvider = new CatalogExportProvider(mainCatalog);

            Container = new CompositionContainer(priorityProvider, mainProvider);
            priorityProvider.SourceProvider = Container;
            mainProvider.SourceProvider = Container;

            var batch = new CompositionBatch();

            BindServices(batch);
            batch.AddExportedValue(mainCatalog);

            Container.Compose(batch);
        }

        protected virtual void BindServices(CompositionBatch batch)
        {
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(Container);
            batch.AddExportedValue(this);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            // https://github.com/Caliburn-Micro/Caliburn.Micro/pull/339
            if (typeof(UIElement).IsAssignableFrom(serviceType))
                return null;

            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = Container.GetExports<object>(contract);

            if (exports.Any())
                return exports.First().Value;

            throw new Exception($"Could not locate any instances of contract {contract}.");
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return Container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            Container.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);
            DisplayRootViewFor<IMainWindow>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] {Assembly.GetEntryAssembly()};
        }
    }
}
