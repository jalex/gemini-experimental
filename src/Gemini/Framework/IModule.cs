#region

using System;
using System.Collections.Generic;
using System.Windows;

#endregion

namespace Gemini.Framework
{
    /// <summary>
    ///     Represents a Gemini framework module for initializing an application component.
    /// </summary>
    public interface IModule
    {

        /// <summary>
        ///     Returns resource dictionaries which should be globally registered.
        /// </summary>
        /// <value>A <see cref="IEnumerable{T}"/> of <see cref="ResourceDictionary"/>.</value>
        IEnumerable<ResourceDictionary> GlobalResourceDictionaries { get; }

        /// <summary>
        ///     Returns a range of documents to load by default.
        /// </summary>
        /// <value>A <see cref="IEnumerable{T}"/> of <see cref="IDocument"/>.</value>
        IEnumerable<IDocument> DefaultDocuments { get; }

        /// <summary>
        ///     Returns a range of tool type contracts to load by default.
        /// </summary>
        /// <value>A <see cref="IEnumerable{T}"/> of <see cref="Type"/>.</value>
        IEnumerable<Type> DefaultTools { get; }

        /// <summary>
        ///     Invoked during the pre-initialization stage of the module. This method
        ///     is being invoked after global resources have been registered.
        /// </summary>
        void PreInitialize();

        /// <summary>
        ///     Invoked during the initialization stage of the module.
        /// </summary>
        void Initialize();

        /// <summary>
        ///     Invoked during the post-initialization stage of the module. This method
        ///     is being invoked after the default types of each module have been loaded.
        /// </summary>
        void PostInitialize();
    }
}
