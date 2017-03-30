#region

using Gemini.Framework.Commands;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner.Commands
{

    /// <summary>
    ///     Represents the <see cref="CommandDefinition"/> for opening the graph.
    /// </summary>
    [CommandDefinition]
    public class OpenGraphCommandDefinition : CommandDefinition
    {

        /// <summary>
        ///     Specifies the name of the command.
        /// </summary>
        public const string CommandName = "File.OpenGraph";

        /// <summary>
        ///     Returns the name of the command.
        /// </summary>
        public override string Name => CommandName;

        /// <summary>
        ///     Returns the display text of the command.
        /// </summary>
        public override string Text => "Open Graph";

        /// <summary>
        ///     Returns a tooltip associated with the command.
        /// </summary>
        public override string ToolTip => "Open Graph";
    }
}
