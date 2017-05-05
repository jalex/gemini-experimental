#region

using System;
using System.Windows.Media.Effects;
using Gemini.Demo.Modules.FilterDesigner.Util;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner.ShaderEffects
{

    /// <summary>
    ///     Represents a base type for a generic <see cref="ShaderEffect"/>.
    /// </summary>
    /// <typeparam name="T">The generic type contract of the <see cref="ShaderEffect"/>.</typeparam>
    internal class ShaderEffectBase<T> : ShaderEffect, IDisposable
    {
        // ReSharper disable once StaticMemberInGenericType
        [ThreadStatic] private static PixelShader _shader;

        private static PixelShader Shader => _shader ?? (_shader = ShaderEffectUtility.GetPixelShader(typeof(T).Name));

        /// <summary>
        ///     Creates a new <see cref="ShaderEffectBase{T}"/>.
        /// </summary>
        protected ShaderEffectBase()
        {
            PixelShader = Shader;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <filterpriority>2</filterpriority>
        void IDisposable.Dispose()
        {
            PixelShader = null;
        }
    }
}
