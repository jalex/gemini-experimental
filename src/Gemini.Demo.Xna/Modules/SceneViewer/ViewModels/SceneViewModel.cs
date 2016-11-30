#region

using System;
using System.ComponentModel.Composition;
using Gemini.Framework;
using Microsoft.Xna.Framework;

#endregion

namespace Gemini.Demo.Xna.Modules.SceneViewer.ViewModels
{
    [Export(typeof(SceneViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SceneViewModel : Document
    {
        private Vector3 _position;

        public SceneViewModel()
        {
            DisplayName = "3D Scene";
        }

        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                NotifyOfPropertyChange(() => Position);
            }
        }

        protected override void OnDeactivate(bool close)
        {
            if (close)
            {
                var view = GetView() as IDisposable;
                if (view != null)
                    view.Dispose();
            }

            base.OnDeactivate(close);
        }
    }
}