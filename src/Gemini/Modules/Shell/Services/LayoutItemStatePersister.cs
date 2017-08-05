#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Modules.Shell.Views;

#endregion

namespace Gemini.Modules.Shell.Services
{
    /// <summary>
    ///     Represents the default implementation of the <see cref="ILayoutItemStatePersister" />.
    /// </summary>
    [Export(typeof(ILayoutItemStatePersister))]
    public class LayoutItemStatePersister : ILayoutItemStatePersister
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(LayoutItemStatePersister));
        private static readonly Type LayoutBaseType = typeof(ILayoutPanel);

        /// <summary>
        ///     Persists the state of the workspace into a file.
        /// </summary>
        /// <param name="shell">The <see cref="IShell" />.</param>
        /// <param name="shellView">The <see cref="IShellView" />.</param>
        /// <param name="fileName">The path of the file into where the state of the workspace will be persisted.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        public bool SaveState(IShell shell, IShellView shellView, string fileName)
        {
            SaveStateInternal(shell, shellView, fileName);

            return true;
        }

        /// <summary>
        ///     Loads the state of the workspace from a file.
        /// </summary>
        /// <param name="shell">The <see cref="IShell" />.</param>
        /// <param name="shellView">The <see cref="IShellView" />.</param>
        /// <param name="fileName">The path of the file from where the state of the workspace will be loaded.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        public bool LoadState(IShell shell, IShellView shellView, string fileName)
        {
            if (!File.Exists(fileName)) return false;

            LoadStateInternal(shell, shellView, fileName);

            return true;
        }

        private static async void SaveStateInternal(IShell shell, IShellView shellView, string fileName)
        {
            try
            {
                //stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                try {
                    var dirname = Path.GetDirectoryName(fileName);
                    Directory.CreateDirectory(dirname);
                    //if (!Directory.Exists(dirname)) {
                        
                    //}
                }
                catch (Exception ex) {
                    Log.Error(ex);
                }
                using (var writer = new BinaryWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write)))
                {
                    var itemStates = shell.Documents.Concat(shell.Tools.Cast<ILayoutPanel>());

                    var itemCount = 0;
                    // reserve some space for items count, it'll be updated later
                    writer.Write(itemCount);

                    foreach (var item in itemStates)
                    {
                        if (!item.ShouldReopenOnStart)
                            continue;

                        var itemType = item.GetType();
                        var exportAttributes = itemType
                            .GetCustomAttributes(typeof(ExportAttribute), false)
                            .Cast<ExportAttribute>().ToList();

                        var exportTypes = new List<Type>();
                        var foundExportContract = false;
                        foreach (var att in exportAttributes)
                        {
                            // select the contract type if it is of type ILayoutitem.
                            var type = att.ContractType;
                            if (LayoutBaseType.IsAssignableFrom(type))
                            {
                                exportTypes.Add(type);
                                foundExportContract = true;
                                continue;
                            }

                            // select the contract name if it is of type ILayoutItem.
                            type = GetTypeFromContractNameAsILayoutItem(att);
                            if (!LayoutBaseType.IsAssignableFrom(type))
                                continue;
                            exportTypes.Add(type);
                            foundExportContract = true;
                        }
                        // select the viewmodel type if it is of type ILayoutItem.
                        if (!foundExportContract && LayoutBaseType.IsAssignableFrom(itemType))
                            exportTypes.Add(itemType);

                        // throw exceptions here, instead of failing silently. These are design time errors.
                        var firstExport = exportTypes.FirstOrDefault();
                        if (firstExport == null)
                            throw new InvalidOperationException(
                                $"A ViewModel that participates in LayoutItem.ShouldReopenOnStart must be decorated with an ExportAttribute who's ContractType that inherits from ILayoutItem, infringing type is {itemType}.");
                        if (exportTypes.Count > 1)
                            throw new InvalidOperationException(
                                $"A ViewModel that participates in LayoutItem.ShouldReopenOnStart can't be decorated with more than one ExportAttribute which inherits from ILayoutItem. infringing type is {itemType}.");

                        var selectedTypeName = firstExport.AssemblyQualifiedName;

                        if (string.IsNullOrEmpty(selectedTypeName))
                            throw new InvalidOperationException(
                                $"Could not retrieve the assembly qualified type name for {firstExport}, most likely because the type is generic.");
                        // TODO: it is possible to save generic types. It requires that every generic parameter is saved, along with its position in the generic tree... A lot of work.

                        writer.Write(selectedTypeName);
                        writer.Write(item.ContentId);

                        // Here's the tricky part. Because some items might fail to save their state, or they might be removed (a plug-in assembly deleted and etc.)
                        // we need to save the item's state size to be able to skip the data during deserialization.
                        // Save current stream position. We'll need it later.
                        var stateSizePosition = writer.BaseStream.Position;

                        // Reserve some space for item state size
                        writer.Write(0L);

                        long stateSize;

                        try
                        {
                            var stateStartPosition = writer.BaseStream.Position;
                            await item.SaveState(writer);
                            stateSize = writer.BaseStream.Position - stateStartPosition;
                        }
                        // ReSharper disable once CatchAllClause
                        catch (Exception ex)
                        {
                            stateSize = 0;

                            Log.Error(ex);
                        }

                        // Go back to the position before item's state and write the actual value.
                        writer.BaseStream.Seek(stateSizePosition, SeekOrigin.Begin);
                        writer.Write(stateSize);

                        if (stateSize > 0)
                            writer.BaseStream.Seek(0, SeekOrigin.End);

                        itemCount++;
                    }

                    writer.BaseStream.Seek(0, SeekOrigin.Begin);
                    writer.Write(itemCount);
                    writer.BaseStream.Seek(0, SeekOrigin.End);

                    shellView.SaveLayout(writer.BaseStream);
                }
            }
            // ReSharper disable once CatchAllClause
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        private static async void LoadStateInternal(IShell shell, IShellView shellView, string fileName)
        {
            var layoutItems = new Dictionary<string, ILayoutPanel>();


            try
            {
                using (var reader = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read)))
                {
                    var count = reader.ReadInt32();

                    for (var i = 0; i < count; i++)
                    {
                        var typeName = reader.ReadString();
                        var contentId = reader.ReadString();
                        var stateEndPosition = reader.ReadInt64();
                        stateEndPosition += reader.BaseStream.Position;

                        var contentType = Type.GetType(typeName);
                        var skipStateData = true;

                        if (contentType != null)
                        {
                            var contentInstance = IoC.GetInstance(contentType, null) as ILayoutPanel;

                            if (contentInstance != null)
                                try
                                {
                                    await contentInstance.LoadState(reader);
                                    layoutItems.Add(contentId, contentInstance);
                                    skipStateData = false;
                                }
                                // ReSharper disable once CatchAllClause
                                catch (Exception ex)
                                {
                                    skipStateData = true;

                                    Log.Error(ex);
                                }
                        }

                        // Skip state data block if we couldn't read it.
                        if (skipStateData)
                            reader.BaseStream.Seek(stateEndPosition, SeekOrigin.Begin);
                    }

                    shellView.LoadLayout(reader.BaseStream, shell.ShowTool, shell.OpenDocument, layoutItems);
                }
            }
            // ReSharper disable once CatchAllClause
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        private static Type GetTypeFromContractNameAsILayoutItem(ExportAttribute attribute)
        {
            if (attribute == null)
                return null;

            string typeName;
            if ((typeName = attribute.ContractName) == null)
                return null;

            var type = Type.GetType(typeName);
            if (type == null || !typeof(ILayoutPanel).IsAssignableFrom(type))
                return null;
            return type;
        }
    }
}
