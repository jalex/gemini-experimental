using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Caliburn.Micro;

namespace Gemini.Framework
{
	public abstract class LayoutItemBase : Screen, ILayoutItem
	{
		private readonly Guid _id = Guid.NewGuid();
		
		public abstract ICommand CloseCommand { get; }

        [Browsable(false)]
		public Guid Id => _id;

	    [Browsable(false)]
		public string ContentId => _id.ToString();

	    [Browsable(false)]
		public virtual Uri IconSource => null;

	    private bool _isSelected;

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