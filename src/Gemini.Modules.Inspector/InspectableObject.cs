#region

using System.Collections.Generic;
using Gemini.Modules.Inspector.Inspectors;

#endregion

namespace Gemini.Modules.Inspector
{
    public class InspectableObject : IInspectableObject
    {
        public InspectableObject(IEnumerable<IInspector> inspectors)
        {
            Inspectors = inspectors;
        }

        public IEnumerable<IInspector> Inspectors { get; set; }
    }
}