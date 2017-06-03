#region

using System.Threading.Tasks;

#endregion

namespace Gemini.Framework
{
    /// <summary>
    ///     Represents an <see cref="IDocument" /> which is persistable as a file on the underlying file system.
    /// </summary>
    public interface IPersistedDocument : IDocument
    {
        /// <summary>
        ///     Returns whether the document is currently in a transient state.
        /// </summary>
        bool IsNew { get; }

        /// <summary>
        ///     Returns whether the document has unpersisted changes.
        /// </summary>
        bool IsDirty { get; }

        /// <summary>
        ///     Returns the file name of the document.
        /// </summary>
        string FileName { get; }

        /// <summary>
        ///     Returns the full file path of the document.
        /// </summary>
        string FilePath { get; }

        /// <summary>
        ///     Asynchronously a new document content using the specified file name.
        /// </summary>
        /// <param name="fileName">The file name of the document.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        Task New(string fileName);

        /// <summary>
        ///     Loads the contents of a file into the document using the specified file path.
        /// </summary>
        /// <param name="filePath">The path of a file to load.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        Task Load(string filePath);

        /// <summary>
        ///     Saves the contents of the the document into a file using the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the file to save.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        Task Save(string filePath);
    }
}
