#region

using System;
using System.Windows.Media.Effects;
using Gemini.Demo.Modules.FilterDesigner.Util;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner.ShaderEffects
{
    internal class ShaderEffectBase<T> : ShaderEffect, IDisposable
    {
        [ThreadStatic] private static PixelShader _shader;

        protected ShaderEffectBase()
        {
            PixelShader = Shader;
        }

        private static PixelShader Shader => _shader ?? (_shader = ShaderEffectUtility.GetPixelShader(typeof(T).Name));

        void IDisposable.Dispose()
        {
            PixelShader = null;
        }
    }
}