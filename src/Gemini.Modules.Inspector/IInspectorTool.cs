#region

using System;
using Gemini.Framework;

#endregion

namespace Gemini.Modules.Inspector
{
    public interface IInspectorTool : ITool
    {
        IInspectableObject SelectedObject { get; set; }
        event EventHandler SelectedObjectChanged;
    }
}
