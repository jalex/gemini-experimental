﻿#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Framework.Themes;
using Gemini.Modules.MainMenu;
using Gemini.Modules.RecentFiles;
using Gemini.Modules.Shell.Services;
using Gemini.Modules.Shell.Views;
using Gemini.Modules.StatusBar;
using Gemini.Modules.ToolBars;

#endregion

namespace Gemini.Modules.Shell.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IDocument>.Collection.OneActive, IShell
    {
        private readonly BindableCollection<ITool> _tools;

        private bool _activateItemGuard;

        private ILayoutPanel _activePanel;
        private bool _closing;

        private IShellView _shellView;

        private bool _showFloatingWindowsInTaskbar;

        public virtual string StateFile => @".\ApplicationState.bin";

        public bool HasPersistedState => File.Exists(StateFile);

        public IMenu MainMenu => _mainMenu;

        public IToolBars ToolBars => _toolBars;

        public IStatusBar StatusBar => _statusBar;

        public IRecentFiles RecentFiles => _recentFiles;

        public ILayoutPanel ActivePanel
        {
            get { return _activePanel; }
            set
            {
                if (ReferenceEquals(_activePanel, value))
                    return;

                _activePanel = value;

                if (value is IDocument)
                    ActivateItem((IDocument) value);

                NotifyOfPropertyChange();
            }
        }

        public IObservableCollection<ITool> Tools => _tools;

        public IDocument SelectedDocument => ActiveItem;

        public IObservableCollection<IDocument> Documents => Items;

        public bool ShowFloatingWindowsInTaskbar
        {
            get { return _showFloatingWindowsInTaskbar; }
            set
            {
                _showFloatingWindowsInTaskbar = value;
                NotifyOfPropertyChange(() => ShowFloatingWindowsInTaskbar);
                _shellView?.UpdateFloatingWindows();
            }
        }

        public ShellViewModel()
        {
            ((IActivate) this).Activate();

            _tools = new BindableCollection<ITool>();
        }

        public event EventHandler ActiveDocumentChanging;
        public event EventHandler ActiveDocumentChanged;

        public void ShowTool<TTool>()
            where TTool : ITool
        {
            ShowTool(IoC.Get<TTool>());
        }

        public void ShowTool(ITool model)
        {
            if (Tools.Contains(model))
                model.IsVisible = true;
            else
                Tools.Add(model);
            model.IsSelected = true;
            ActivePanel = model;
        }

        public bool TryActivateDocumentByPath(string path)
        {
            foreach (var document in Documents.OfType<PersistedDocument>().Where(d => !d.IsNew))
            {
                if (string.IsNullOrEmpty(document.FilePath))
                    continue;

                var docPath = Path.GetFullPath(document.FilePath);
                if (string.Equals(path, docPath, StringComparison.OrdinalIgnoreCase))
                {
                    OpenDocument(document);
                    return true;
                }
            }

            return false;
        }

        public void OpenDocument(IDocument model)
        {
            ActivateItem(model);
        }

        public void CloseDocument(IDocument document)
        {
            DeactivateItem(document, true);
        }

        public void Close()
        {
            Application.Current.MainWindow.Close();
        }

        protected override void OnViewLoaded(object view)
        {
            foreach (var module in _modules)
            foreach (var globalResourceDictionary in module.GlobalResourceDictionaries)
                Application.Current.Resources.MergedDictionaries.Add(globalResourceDictionary);

            foreach (var module in _modules)
                module.PreInitialize();
            foreach (var module in _modules)
                module.Initialize();

            // If after initialization no theme was loaded, load the default one
            if (_themeManager.CurrentTheme == null)
                if (!_themeManager.SetCurrentTheme(Properties.Settings.Default.ThemeName))
                {
                    Properties.Settings.Default.ThemeName =
                        (string) Properties.Settings.Default.Properties["ThemeName"].DefaultValue;
                    Properties.Settings.Default.Save();
                    if (!_themeManager.SetCurrentTheme(Properties.Settings.Default.ThemeName))
                        throw new InvalidOperationException("unable to load application theme");
                }

            _shellView = (IShellView) view;
            if (!_layoutItemStatePersister.LoadState(this, _shellView, StateFile))
            {
                foreach (var defaultDocument in _modules.SelectMany(x => x.DefaultDocuments))
                    OpenDocument(defaultDocument);
                foreach (var defaultTool in _modules.SelectMany(x => x.DefaultTools))
                    ShowTool((ITool) IoC.GetInstance(defaultTool, null));
            }

            foreach (var module in _modules)
                module.PostInitialize();

            base.OnViewLoaded(view);
        }

        public override void ActivateItem(IDocument item)
        {
            if (_closing || _activateItemGuard)
                return;

            _activateItemGuard = true;

            try
            {
                if (ReferenceEquals(item, ActiveItem))
                    return;

                RaiseActiveDocumentChanging();

                base.ActivateItem(item);

                RaiseActiveDocumentChanged();
            }
            finally
            {
                _activateItemGuard = false;
            }
        }

        private void RaiseActiveDocumentChanging()
        {
            var handler = ActiveDocumentChanging;
            handler?.Invoke(this, EventArgs.Empty);
        }

        private void RaiseActiveDocumentChanged()
        {
            var handler = ActiveDocumentChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnActivationProcessed(IDocument item, bool success)
        {
            if (!ReferenceEquals(ActivePanel, item))
                ActivePanel = item;

            base.OnActivationProcessed(item, success);
        }

        public override void DeactivateItem(IDocument item, bool close)
        {
            RaiseActiveDocumentChanging();

            try
            {
                // TODO since the update to latest Xceed.AvalonDock we get an exception here now. ignore
                base.DeactivateItem(item, close);
            }
            catch
            {
            }

            RaiseActiveDocumentChanged();
        }

        protected override void OnDeactivate(bool close)
        {
            // Workaround for a complex bug that occurs when
            // (a) the window has multiple documents open, and
            // (b) the last document is NOT active
            //
            // The issue manifests itself with a crash in
            // the call to base.ActivateItem(item), above,
            // saying that the collection can't be changed
            // in a CollectionChanged event handler.
            //
            // The issue occurs because:
            // - Caliburn.Micro sees the window is closing, and calls Items.Clear()
            // - AvalonDock handles the CollectionChanged event, and calls Remove()
            //   on each of the open documents.
            // - If removing a document causes another to become active, then AvalonDock
            //   sets a new ActiveContent.
            // - We have a WPF binding from Caliburn.Micro's ActiveItem to AvalonDock's
            //   ActiveContent property, so ActiveItem gets updated.
            // - The document no longer exists in Items, beacuse that collection was cleared,
            //   but Caliburn.Micro helpfully adds it again - which causes the crash.
            //
            // My workaround is to use the following _closing variable, and ignore activation
            // requests that occur when _closing is true.
            _closing = true;

            _layoutItemStatePersister.SaveState(this, _shellView, StateFile);

            try
            {
                // TODO argument null exception occurs where, but I'm not sure why
                //      -> investigate
                base.OnDeactivate(close);
            }
            catch
            {
            }
        }

#pragma warning disable 649
        [ImportMany(typeof(IModule))] private IEnumerable<IModule> _modules;

        [Import] private IThemeManager _themeManager;

        [Import] private IMenu _mainMenu;

        [Import] private IToolBars _toolBars;

        [Import] private IStatusBar _statusBar;

        [Import] private IRecentFiles _recentFiles;

        [Import] private ILayoutItemStatePersister _layoutItemStatePersister;
#pragma warning restore 649
    }
}
