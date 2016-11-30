#region

using System.Windows;
using System.Windows.Media;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner.ShaderEffects
{
    internal class AddEffect : ShaderEffectBase<AddEffect>
    {
        public static readonly DependencyProperty Input1Property = RegisterPixelShaderSamplerProperty(
            "Input1", typeof(AddEffect), 0);

        public static readonly DependencyProperty Input2Property = RegisterPixelShaderSamplerProperty(
            "Input2", typeof(AddEffect), 1);

        public AddEffect()
        {
            UpdateShaderValue(Input1Property);
            UpdateShaderValue(Input2Property);
        }

        public Brush Input1
        {
            get { return (Brush) GetValue(Input1Property); }
            set { SetValue(Input1Property, value); }
        }

        public Brush Input2
        {
            get { return (Brush) GetValue(Input2Property); }
            set { SetValue(Input2Property, value); }
        }
    }
}