#region

using System;
using System.Collections.Generic;
using System.Windows;

#endregion

namespace Gemini.Framework
{
    public interface IModule
    {
        IEnumerable<ResourceDictionary> GlobalResourceDictionaries { get; }
        IEnumerable<IDocument> DefaultDocuments { get; }
        IEnumerable<Type> DefaultTools { get; }

        void PreInitialize();
        void Initialize();
        void PostInitialize();
    }
}