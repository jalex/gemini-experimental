#region

using System.Collections;
using System.Collections.Generic;
using Caliburn.Micro;

#endregion

namespace Gemini.Modules.MainMenu.Models
{
    public class MenuItemBase : PropertyChangedBase, IEnumerable<MenuItemBase>
    {
        #region Static stuff

        public static MenuItemBase Separator => new MenuItemSeparator();

        #endregion

        #region Properties

        public IObservableCollection<MenuItemBase> Children { get; }

        #endregion

        #region Constructors

        protected MenuItemBase()
        {
            Children = new BindableCollection<MenuItemBase>();
        }

        #endregion

        public IEnumerator<MenuItemBase> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(params MenuItemBase[] menuItems)
        {
            menuItems.Apply(Children.Add);
        }
    }
}
