#region

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Gemini.Demo.Xna.Modules.SceneViewer.ViewModels;
using Gemini.Framework;
using Gemini.Modules.Inspector;
using Gemini.Modules.Inspector.Xna;

#endregion

namespace Gemini.Demo.Xna.Modules.SceneViewer
{

    /// <summary>
    ///     Represents the default <see cref="IModule"/> of the scene viewer.
    /// </summary>
    [Export(typeof(IModule))]
    public class Module : ModuleBase
    {
        private readonly IInspectorTool _inspectorTool;

        /// <summary>
        ///     Returns a range of documents to load by default.
        /// </summary>
        /// <value>A <see cref="IEnumerable{T}"/> of <see cref="IDocument"/>.</value>
        public override IEnumerable<IDocument> DefaultDocuments
        {
            get { yield return new SceneViewModel(); }
        }

        /// <summary>
        ///     Creates a new <see cref="Module"/>.
        /// </summary>
        /// <param name="inspectorTool">The <see cref="IInspectorTool"/>.</param>
        [ImportingConstructor]
        public Module(IInspectorTool inspectorTool)
        {
            _inspectorTool = inspectorTool;
        }

        /// <summary>
        ///     Invoked during the initialization stage of the module.
        /// </summary>
        public override void Initialize()
        {
            var sceneViewModel = Shell.Documents.OfType<SceneViewModel>().FirstOrDefault();
            if (sceneViewModel != null)
                _inspectorTool.SelectedObject = new InspectableObjectBuilder()
                    .WithVector3Editor(sceneViewModel, x => x.Position)
                    .ToInspectableObject();
        }
    }
}
