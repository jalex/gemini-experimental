#region

using System.Collections.Generic;

#endregion

namespace Gemini.Modules.Inspector.Inspectors
{
    public class CollapsibleGroupViewModel : InspectorBase
    {
        private static readonly Dictionary<string, bool> PersistedExpandCollapseStates = new Dictionary<string, bool>();

        private readonly string _name;

        private bool _isExpanded;

        public CollapsibleGroupViewModel(string name, IEnumerable<IInspector> children)
        {
            _name = name;
            Children = children;

            if (!PersistedExpandCollapseStates.TryGetValue(_name, out _isExpanded))
                _isExpanded = true;
        }

        public override string Name => _name;

        public override bool IsReadOnly => false;

        public IEnumerable<IInspector> Children { get; }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                PersistedExpandCollapseStates[_name] = value;
                    // TODO: Key should be full path to this group, not just the name.
                NotifyOfPropertyChange();
            }
        }
    }
}