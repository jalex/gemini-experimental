#region

using Gemini.Framework;

#endregion

namespace Gemini.Modules.PropertyGrid
{
    public interface IPropertyGrid : ITool
    {
        object SelectedObject { get; set; }
    }
}