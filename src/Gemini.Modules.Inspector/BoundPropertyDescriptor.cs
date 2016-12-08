#region

using System;
using System.ComponentModel;

#endregion

namespace Gemini.Modules.Inspector
{
    public class BoundPropertyDescriptor
    {
        public PropertyDescriptor PropertyDescriptor { get; }
        public object PropertyOwner { get; }

        public object Value
        {
            get { return PropertyDescriptor.GetValue(PropertyOwner); }
            set { PropertyDescriptor.SetValue(PropertyOwner, value); }
        }

        public BoundPropertyDescriptor(object propertyOwner, PropertyDescriptor propertyDescriptor)
        {
            PropertyOwner = propertyOwner;
            PropertyDescriptor = propertyDescriptor;
        }

        public event EventHandler ValueChanged
        {
            add { PropertyDescriptor.AddValueChanged(PropertyOwner, value); }
            remove { PropertyDescriptor.RemoveValueChanged(PropertyOwner, value); }
        }

        public static BoundPropertyDescriptor FromProperty(object propertyOwner, string propertyName)
        {
            // TODO: Cache all this.
            var properties = TypeDescriptor.GetProperties(propertyOwner);
            return new BoundPropertyDescriptor(propertyOwner, properties.Find(propertyName, false));
        }
    }
}