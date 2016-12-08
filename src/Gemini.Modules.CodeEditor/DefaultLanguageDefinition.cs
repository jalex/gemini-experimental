#region

using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

#endregion

namespace Gemini.Modules.CodeEditor
{
    internal class DefaultLanguageDefinition : ILanguageDefinition
    {
        private IHighlightingDefinition _highlightingDefinition;
        private XshdSyntaxDefinition _syntaxDefinition;

        public string Name { get; }

        public IEnumerable<string> FileExtensions { get; set; }

        public IHighlightingDefinition SyntaxHighlighting
            => _highlightingDefinition ?? (_highlightingDefinition = LoadHighlightingDefinition());

        public string CustomSyntaxHighlightingFileName { get; set; }

        public DefaultLanguageDefinition(string name, IEnumerable<string> fileExtensions)
        {
            Name = name;
            FileExtensions = fileExtensions;
        }

        public DefaultLanguageDefinition(XshdSyntaxDefinition syntaxDefinition)
            : this(syntaxDefinition.Name, syntaxDefinition.Extensions)
        {
            _syntaxDefinition = syntaxDefinition;
        }

        private IHighlightingDefinition LoadHighlightingDefinition()
        {
            var highlightingManager = HighlightingManager.Instance;

            if (!string.IsNullOrEmpty(CustomSyntaxHighlightingFileName))
                using (var reader = new XmlTextReader(CustomSyntaxHighlightingFileName))
                {
                    _syntaxDefinition = HighlightingLoader.LoadXshd(reader);
                }

            if (_syntaxDefinition != null)
            {
                var highlightingDefinition =
                    HighlightingLoader.Load(_syntaxDefinition, highlightingManager);

                highlightingManager.RegisterHighlighting(_syntaxDefinition.Name,
                    _syntaxDefinition.Extensions.ToArray(),
                    highlightingDefinition);
            }

            return highlightingManager.GetDefinition(Name);
        }
    }
}