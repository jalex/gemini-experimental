#region

using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;

#endregion

namespace Gemini.Modules.CodeCompiler
{
    public interface ICodeCompiler
    {
        Assembly Compile(
            IEnumerable<SyntaxTree> syntaxTrees,
            IEnumerable<MetadataReference> references,
            string outputName,
            bool exportDll = false,
            string exportDir = null);
    }
}