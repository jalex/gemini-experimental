﻿#region

using System.ComponentModel.Composition;
using Caliburn.Micro;
using Gemini.Modules.ToolBars.Controls;
using Gemini.Modules.ToolBars.Views;

#endregion

namespace Gemini.Modules.ToolBars.ViewModels
{
    [Export(typeof(IToolBars))]
    public class ToolBarsViewModel : ViewAware, IToolBars
    {
        private readonly BindableCollection<IToolBar> _items;

        private readonly IToolBarBuilder _toolBarBuilder;

        private bool _locked;

        private bool _visible;

        public IObservableCollection<IToolBar> Items => _items;

        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                NotifyOfPropertyChange();
            }
        }

        public bool Locked
        {
            get { return _locked; }
            set
            {
                _locked = value;
                NotifyOfPropertyChange();
            }
        }

        [ImportingConstructor]
        public ToolBarsViewModel(IToolBarBuilder toolBarBuilder)
        {
            _toolBarBuilder = toolBarBuilder;
            _items = new BindableCollection<IToolBar>();
            _locked = false;
        }

        protected override void OnViewLoaded(object view)
        {
            _toolBarBuilder.BuildToolBars(this);

            // TODO: Ideally, the ToolBarTray control would expose ToolBars
            // as a dependency property. We could use an attached property
            // to workaround this. But for now, toolbars need to be
            // created prior to the following code being run.
            foreach (var toolBar in Items)
                ((IToolBarsView) view).ToolBarTray.ToolBars.Add(new MainToolBar
                {
                    ItemsSource = toolBar
                });

            base.OnViewLoaded(view);
        }
    }
}
