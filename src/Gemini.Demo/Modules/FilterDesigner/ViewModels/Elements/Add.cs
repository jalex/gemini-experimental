#region

using System.Windows.Media;
using System.Windows.Media.Effects;
using Gemini.Demo.Modules.FilterDesigner.ShaderEffects;
using Gemini.Modules.Toolbox;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner.ViewModels.Elements
{
    [ToolboxItem(typeof(GraphViewModel), "Add", "Maths",
        "pack://application:,,,/Modules/FilterDesigner/Resources/action_add_16xLG.png")]
    public class Add : ShaderEffectElement
    {
        public Add()
        {
            AddInputConnector("Left", Colors.DarkSeaGreen);
            AddInputConnector("Right", Colors.DarkSeaGreen);
        }

        protected override Effect GetEffect()
        {
            return new AddEffect
            {
                Input1 = new ImageBrush(InputConnectors[0].Value),
                Input2 = new ImageBrush(InputConnectors[1].Value)
            };
        }
    }
}
