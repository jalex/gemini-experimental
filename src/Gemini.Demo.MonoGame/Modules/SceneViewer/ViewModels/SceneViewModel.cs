#region

using System;
using System.ComponentModel.Composition;
using Gemini.Demo.MonoGame.Modules.SceneViewer.Views;
using Gemini.Framework;
using Microsoft.Xna.Framework;

#endregion

namespace Gemini.Demo.MonoGame.Modules.SceneViewer.ViewModels
{
    /// <summary>
    ///     Represents the view model of the scene view.
    /// </summary>
    [Export(typeof(SceneViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SceneViewModel : Document
    {
        private Vector3 _position;
        private ISceneView _sceneView;

        /// <summary>
        ///     Returns whether the panel should be re-opened when the application starts.
        /// </summary>
        public override bool ShouldReopenOnStart => true;

        /// <summary>
        ///     Returns the position of the model.
        /// </summary>
        /// <value>A <see cref="Vector3" />.</value>
        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                NotifyOfPropertyChange(() => Position);

                _sceneView?.Invalidate();
            }
        }

        /// <summary>
        ///     Creates a new <see cref="SceneViewModel" />.
        /// </summary>
        public SceneViewModel()
        {
            DisplayName = "3D Scene";
        }

        /// <summary>Called when an attached view's Loaded event fires.</summary>
        /// <param name="view"></param>
        protected override void OnViewLoaded(object view)
        {
            _sceneView = view as ISceneView;
            base.OnViewLoaded(view);
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
