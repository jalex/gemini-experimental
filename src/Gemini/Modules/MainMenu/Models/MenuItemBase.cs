#region

using System.Collections;
using System.Collections.Generic;
using Caliburn.Micro;
using System.Runtime.CompilerServices;

#endregion

namespace Gemini.Modules.MainMenu.Models
{
    public class MenuItemBase : PropertyChangedBase, IEnumerable<MenuItemBase>
    {
        #region Static stuff

        public static MenuItemBase Separator => new MenuItemSeparator();

        #endregion

        #region Properties

        public virtual IObservableCollection<MenuItemBase> Children { get; }

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

        #region from Dynamic Data AbstractNotifyPropertyChanged
        
        /// <summary>
        /// If the value has changed, sets referenced backing field and raise notify property changed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingField">The backing field.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void SetAndRaise<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            SetAndRaise(ref backingField, newValue, EqualityComparer<T>.Default, propertyName);
        }
        /// <summary>
        /// If the value has changed, sets referenced backing field and raise notify property changed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingField">The backing field.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="comparer">The comparer.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void SetAndRaise<T>(ref T backingField, T newValue, IEqualityComparer<T> comparer, [CallerMemberName] string propertyName = null)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;
            if (comparer.Equals(backingField, newValue)) return;
            backingField = newValue;
            NotifyOfPropertyChange(propertyName);
        }

        #endregion
    }
}
