#region

using System.Windows.Input;
using Caliburn.Micro;
using Gemini.Framework.Services;
using Gemini.Framework.ToolBars;
using Gemini.Modules.ToolBars;
using Gemini.Modules.ToolBars.Models;

#endregion

namespace Gemini.Framework
{
    public abstract class Tool : LayoutItemBase, ITool
    {
        private ICommand _closeCommand;

        private bool _isVisible;

        private IToolBar _toolBar;

        private ToolBarDefinition _toolBarDefinition;

        public ToolBarDefinition ToolBarDefinition
        {
            get { return _toolBarDefinition; }
            protected set
            {
                _toolBarDefinition = value;
                NotifyOfPropertyChange(() => ToolBar);
                NotifyOfPropertyChange();
            }
        }

        public IToolBar ToolBar
        {
            get
            {
                if (_toolBar != null)
                    return _toolBar;

                if (ToolBarDefinition == null)
                    return null;

                var toolBarBuilder = IoC.Get<IToolBarBuilder>();
                _toolBar = new ToolBarModel();
                toolBarBuilder.BuildToolBar(ToolBarDefinition, _toolBar);
                return _toolBar;
            }
        }

        public override ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(p => IsVisible = false, p => true)); }
        }

        public abstract PaneLocation PreferredLocation { get; }

        public virtual double PreferredWidth => 200;

        public virtual double PreferredHeight => 200;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyOfPropertyChange(() => IsVisible);
            }
        }

        public override bool ShouldReopenOnStart => true;

        protected Tool()
        {
            IsVisible = true;
        }
    }
}
