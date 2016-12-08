#region

using System;
using System.ComponentModel.Composition;
using Gemini.Demo.MonoGame.Modules.SceneViewer.Views;
using Gemini.Framework;
using Microsoft.Xna.Framework;

#endregion

namespace Gemini.Demo.MonoGame.Modules.SceneViewer.ViewModels
{
    [Export(typeof(SceneViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SceneViewModel : Document
    {
        private Vector3 _position;
        private ISceneView _sceneView;

        public override bool ShouldReopenOnStart => true;

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

        public SceneViewModel()
        {
            DisplayName = "3D Scene";
        }

        protected override void OnViewLoaded(object view)
        {
            _sceneView = view as ISceneView;
            base.OnViewLoaded(view);
        }

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