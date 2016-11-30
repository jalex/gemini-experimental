#region

using System.Collections.Generic;
using Gemini.Modules.Inspector.Inspectors;

#endregion

namespace Gemini.Modules.Inspector
{
    public interface IInspectableObject
    {
        IEnumerable<IInspector> Inspectors { get; }
    }
}