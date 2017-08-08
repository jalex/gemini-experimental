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

#endregion

namespace Gemini
{
    public class AppBootstrapper : BootstrapperBase
    {
        private List<Assembly> _priorityAssemblies;
        protected CompositionContainer Container { get; set; }
        internal IList<Assembly> PriorityAssemblies => _priorityAssemblies;

        public AppBootstrapper()
        {
            PreInitialize();
            Initialize();
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
        /// <summary>
        ///     By default, we are configured to use MEF
        /// </summary>
        protected override void Configure()  {
            // Add all assemblies to AssemblySource (using a temporary DirectoryCatalog).
            var directoryCatalog = new DirectoryCatalog(CatalogPath);
            
            AssemblySource.Instance.AddRange(
            directoryCatalog.Parts
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
