#region

using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using Gemini.Framework.Threading;

#endregion

namespace Gemini.Framework
{
    public abstract class LayoutItemBase : Screen, ILayoutPanel
    {
        private bool _isSelected;

        public abstract ICommand CloseCommand { get; }

        [Browsable(false)]
        public Guid Id { get; } = Guid.NewGuid();

        [Browsable(false)]
        public virtual string ToolTip { get { return DisplayName; } }

        [Browsable(false)]
        public string ContentId => Id.ToString();

        [Browsable(false)]
        public virtual Uri IconSource => null;

        [Browsable(false)]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        [Browsable(false)]
        public virtual bool ShouldReopenOnStart => false;

        public virtual Task LoadState(BinaryReader reader)
        {
            return TaskUtility.Completed;
        }

        public virtual Task SaveState(BinaryWriter writer)
        {
            return TaskUtility.Completed;
        }
    }
}