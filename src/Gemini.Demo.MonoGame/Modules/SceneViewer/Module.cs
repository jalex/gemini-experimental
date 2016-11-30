#region

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Gemini.Demo.MonoGame.Modules.SceneViewer.ViewModels;
using Gemini.Framework;
using Gemini.Modules.Inspector;
using Gemini.Modules.Inspector.MonoGame;

#endregion

namespace Gemini.Demo.MonoGame.Modules.SceneViewer
{
    [Export(typeof(IModule))]
    public class Module : ModuleBase
    {
        private readonly IInspectorTool _inspectorTool;

        [ImportingConstructor]
        public Module(IInspectorTool inspectorTool)
        {
            _inspectorTool = inspectorTool;
        }

        public override IEnumerable<IDocument> DefaultDocuments
        {
            get { yield return new SceneViewModel(); }
        }

        public override void PostInitialize()
        {
            var sceneViewModel = Shell.Documents.OfType<SceneViewModel>().FirstOrDefault();
            if (sceneViewModel != null)
                _inspectorTool.SelectedObject = new InspectableObjectBuilder()
                    .WithVector3Editor(sceneViewModel, x => x.Position)
                    .ToInspectableObject();
        }
    }
}