#region

using System;
using System.ComponentModel.Composition;
using Gemini.Framework;
using Microsoft.Xna.Framework;

#endregion

namespace Gemini.Demo.Xna.Modules.SceneViewer.ViewModels
{

    /// <summary>
    ///     Rperesents the view model of the scene.
    /// </summary>
    [Export(typeof(SceneViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SceneViewModel : Document
    {
        private Vector3 _position;

        /// <summary>
        ///     Returns the position of the model as <see cref="Vector3"/>.
        /// </summary>
        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                NotifyOfPropertyChange(() => Position);
            }
        }

        /// <summary>
        ///     Creates a new <see cref="SceneViewModel"/>.
        /// </summary>
        public SceneViewModel()
        {
            DisplayName = "3D Scene";
        }

        /// <summary>Called when deactivating.</summary>
        /// <param name="close">Inidicates whether this instance will be closed.</param>
        protected override void OnDeactivate(bool close)
        {
            if (close)
            {
                var view = GetView() as IDisposable;
                view?.Dispose();
            }

            base.OnDeactivate(close);
        }
    }
}
