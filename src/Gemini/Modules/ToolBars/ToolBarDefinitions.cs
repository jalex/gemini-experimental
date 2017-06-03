#region

using System.ComponentModel.Composition;
using Gemini.Framework.ToolBars;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.ToolBars
{
    internal static class ToolBarDefinitions
    {
        [Export] public static ToolBarDefinition StandardToolBar = new ToolBarDefinition(0, Resources.ToolBarStandard);
    }
}
