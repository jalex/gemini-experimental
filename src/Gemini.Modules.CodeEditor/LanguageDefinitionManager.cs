#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml;
using Caliburn.Micro;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

#endregion

namespace Gemini.Modules.CodeEditor
{
    [Export(typeof(LanguageDefinitionManager))]
    public class LanguageDefinitionManager
    {
        private List<ILanguageDefinition> _languageDefinitions;

        public IEnumerable<ILanguageDefinition> LanguageDefinitions
            => _languageDefinitions ?? (_languageDefinitions = Initialize());

        public ILanguageDefinition GetDefinitionByExtension(string extension)
        {
            return LanguageDefinitions.FirstOrDefault(l => l.FileExtensions.Contains(extension));
        }

        private List<ILanguageDefinition> Initialize()
        {
            // Create built in language definitions
            var languageDefinitions = new List<ILanguageDefinition>
            {
                new DefaultLanguageDefinition("C#", new[] {".cs"}),
                new DefaultLanguageDefinition("JavaScript", new[] {".js"}),
                new DefaultLanguageDefinition("HTML", new[] {".htm", ".html"}),
                new DefaultLanguageDefinition("ASP/XHTML",
                    new[] {".asp", ".aspx", ".asax", ".asmx", ".ascx", ".master"}),
                new DefaultLanguageDefinition("Boo", new[] {".boo"}),
                new DefaultLanguageDefinition("Coco", new[] {".atg"}),
                new DefaultLanguageDefinition("CSS", new[] {".css"}),
                new DefaultLanguageDefinition("C++", new[] {".c", ".h", ".cc", ".cpp", ".hpp"}),
                new DefaultLanguageDefinition("Java", new[] {".java"}),
                new DefaultLanguageDefinition("Patch", new[] {".patch", ".diff"}),
                new DefaultLanguageDefinition("PowerShell", new[] {".ps1", ".psm1", ".psd1"}),
                new DefaultLanguageDefinition("PHP", new[] {".php"}),
                new DefaultLanguageDefinition("TeX", new[] {".tex"}),
                new DefaultLanguageDefinition("VBNET", new[] {".vb"}),
                new DefaultLanguageDefinition("XML", (".xml;.xsl;.xslt;.xsd;.manifest;.config;.addin;" +
                                                      ".xshd;.wxs;.wxi;.wxl;.proj;.csproj;.vbproj;.ilproj;" +
                                                      ".booproj;.build;.xfrm;.targets;.xaml;.xpt;" +
                                                      ".xft;.map;.wsdl;.disco;.ps1xml;.nuspec").Split(';')),
                new DefaultLanguageDefinition("MarkDown", new[] {".md"})
            };

            // Add imported definitions
            foreach (var importedLanguage in IoC.GetAll<ILanguageDefinition>())
            {
                var defaultLanguage =
                    languageDefinitions.FirstOrDefault(
                        l => string.Equals(l.Name, importedLanguage.Name, StringComparison.InvariantCultureIgnoreCase));

                if (defaultLanguage != null)
                    languageDefinitions.Remove(defaultLanguage);

                languageDefinitions.Add(importedLanguage);
            }

            // Scan SyntaxHighlighting folder for more languages
            var path = Path.Combine(Directory.GetCurrentDirectory(), "SyntaxHighlighting");

            if (!Directory.Exists(path))
                return languageDefinitions;

            var highlightingFiles = Directory.GetFiles(path, "*.xshd").ToList();
            var invalidFileNameChars = Path.GetInvalidFileNameChars();

            var definitions = languageDefinitions.ToDictionary(l =>
            {
                var nameChars = l.Name.ToCharArray();
                nameChars = nameChars.Except(invalidFileNameChars).ToArray();
                return new string(nameChars);
            }, l => l);

            foreach (var highlightingFile in highlightingFiles)
            {
                var fileName = Path.GetFileNameWithoutExtension(highlightingFile);

                if (string.IsNullOrEmpty(fileName))
                    continue;

                ILanguageDefinition definition;
                if (definitions.TryGetValue(fileName, out definition))
                    definition.CustomSyntaxHighlightingFileName = highlightingFile;
                else
                    try
                    {
                        XshdSyntaxDefinition syntaxDefinition;

                        using (var reader = new XmlTextReader(highlightingFile))
                        {
                            syntaxDefinition = HighlightingLoader.LoadXshd(reader);
                        }

                        // Create language based on highlighting file.
                        languageDefinitions.Add(new DefaultLanguageDefinition(syntaxDefinition));
                    }
                    catch
                    {
                    }
            }

            return languageDefinitions;
        }
    }
}