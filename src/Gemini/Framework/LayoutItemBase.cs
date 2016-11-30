#region

using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Caliburn.Micro;

#endregion

namespace Gemini.Framework
{
    public abstract class LayoutItemBase : Screen, ILayoutItem
    {
        private bool _isSelected;

        public abstract ICommand CloseCommand { get; }

        [Browsable(false)]
        public Guid Id { get; } = Guid.NewGuid();

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

        public virtual void LoadState(BinaryReader reader)
        {
        }

        public virtual void SaveState(BinaryWriter writer)
        {
        }
    }
}