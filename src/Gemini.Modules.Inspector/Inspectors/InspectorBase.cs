#region

using Caliburn.Micro;

#endregion

namespace Gemini.Modules.Inspector.Inspectors
{
    public abstract class InspectorBase : PropertyChangedBase, IInspector
    {
        public abstract string Name { get; }
        public abstract bool IsReadOnly { get; }
    }
}
