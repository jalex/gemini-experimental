using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;
using Caliburn.Micro;

namespace Gemini.Modules.Inspector.Inspectors
{
    public class InspectorList : PropertyChangedBase
    {
        private ObservableCollection<string> _listCollection = new ObservableCollection<string>();
        [XmlIgnore]
        public ObservableCollection<string> ListCollection
        {
            get { return _listCollection; }
            set
            {
                _listCollection = value;
                NotifyOfPropertyChange(() => ListCollection);
            }
        }

        private string _selectedValue;
        public string SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                _selectedValue = value;
                NotifyOfPropertyChange(() => SelectedValue); 
            }
        }
    }

    public class InspectorListEditorViewModel : EditorBase<InspectorList>, ILabelledInspector
    {
        public ObservableCollection<string> ListCollection => Value.ListCollection;

        public string SelectedValue
        {
            get { return Value.SelectedValue; }
            set
            {
                Value.SelectedValue = value;
                NotifyOfPropertyChange(() => SelectedValue);
            }
        }
    }
}
