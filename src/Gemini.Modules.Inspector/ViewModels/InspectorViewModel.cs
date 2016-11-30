#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Modules.Inspector.Inspectors;
using Gemini.Modules.Inspector.Properties;

#endregion

namespace Gemini.Modules.Inspector.ViewModels
{
    [Export(typeof(IInspectorTool))]
    public class InspectorViewModel : Tool, IInspectorTool
    {
        private IInspectableObject _selectedObject;

        public InspectorViewModel()
        {
            DisplayName = Resources.InspectorDisplayName;
        }

        public event EventHandler SelectedObjectChanged;

        public override PaneLocation PreferredLocation => PaneLocation.Right;

        public override double PreferredWidth => 300;

        public IInspectableObject SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                NotifyOfPropertyChange(() => SelectedObject);
                RaiseSelectedObjectChanged();
            }
        }

        private void RaiseSelectedObjectChanged()
        {
            var handler = SelectedObjectChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void ResetAll()
        {
            if (SelectedObject == null)
                return;

            RecurseEditors(SelectedObject.Inspectors, delegate(IEditor editor)
            {
                if ((editor != null) && editor.CanReset)
                    editor.Reset();
            });
        }

        public void RecurseEditors(IEnumerable<IInspector> inspectors, Action<IEditor> action)
        {
            foreach (var inspector in inspectors)
            {
                var group = inspector as CollapsibleGroupViewModel;
                if (group != null)
                    RecurseEditors(group.Children, action);
                else
                    action(inspector as IEditor);
            }
        }
    }
}