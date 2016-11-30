#region

using System.ComponentModel.Composition;
using Gemini.Framework;
using SharpDX;

#endregion

namespace Gemini.Demo.SharpDX.Modules.SceneViewer.ViewModels
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

        public override bool ShouldReopenOnStart => true;

        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                NotifyOfPropertyChange(() => Position);
            }
        }
    }
}