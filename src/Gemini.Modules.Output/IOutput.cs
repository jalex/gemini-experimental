#region

using System.IO;
using Gemini.Framework;

#endregion

namespace Gemini.Modules.Output
{
    public interface IOutput : ITool
    {
        TextWriter Writer { get; }
        void AppendLine(string text);
        void Append(string text);
        void Clear();
    }
}
