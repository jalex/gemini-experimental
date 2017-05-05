#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Gemini.Framework.Services;
using Gemini.Modules.MainMenu;
using Gemini.Modules.ToolBars;

#endregion

namespace Gemini.Framework
{

    /// <summary>
    ///     Represents a base implementation of a <see cref="IModule"/>.
    /// </summary>
    public abstract class ModuleBase : IModule
    {

        /// <summary>
        ///     Returns the <see cref="IMainWindow"/> of the application.
        /// </summary>
        protected IMainWindow MainWindow => _mainWindow;

        /// <summary>
        ///     Returns the <see cref="IShell"/> of the application.
        /// </summary>
        protected IShell Shell => _shell;

        /// <summary>
        ///     Returns the <see cref="IMenu"/> of the shell.
        /// </summary>
        protected IMenu MainMenu => _shell.MainMenu;

        /// <summary>
        ///     Returns the <see cref="IToolBars"/>s of the shell.
        /// </summary>
        protected IToolBars ToolBars => _shell.ToolBars;

        /// <summary>
        ///     Returns resource dictionaries which should be globally registered.
        /// </summary>
        /// <value>A <see cref="IEnumerable{T}"/> of <see cref="ResourceDictionary"/>.</value>
        public virtual IEnumerable<ResourceDictionary> GlobalResourceDictionaries
        {
            get { yield break; }
        }

        /// <summary>
        ///     Returns a range of documents to load by default.
        /// </summary>
        /// <value>A <see cref="IEnumerable{T}"/> of <see cref="IDocument"/>.</value>
        public virtual IEnumerable<IDocument> DefaultDocuments
        {
            get { yield break; }
        }

        /// <summary>
        ///     Returns a range of tool type contracts to load by default.
        /// </summary>
        /// <value>A <see cref="IEnumerable{T}"/> of <see cref="Type"/>.</value>
        public virtual IEnumerable<Type> DefaultTools
        {
            get { yield break; }
        }

        /// <summary>
        ///     Invoked during the pre-initialization stage of the module. This method
        ///     is being invoked after global resources have been registered.
        /// </summary>
        public virtual void PreInitialize()
        {
        }

        /// <summary>
        ///     Invoked during the initialization stage of the module.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        ///     Invoked during the post-initialization stage of the module. This method
        ///     is being invoked after the default types of each module have been loaded.
        /// </summary>
        public virtual void PostInitialize()
        {
        }
#pragma warning disable 649
        [Import] private IMainWindow _mainWindow;

        [Import] private IShell _shell;
#pragma warning restore 649
    }
}
