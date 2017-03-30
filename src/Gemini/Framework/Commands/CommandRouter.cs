﻿#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using Gemini.Framework.Services;

#endregion

namespace Gemini.Framework.Commands
{
    /// <summary>
    ///     Represents the default implementation of the <see cref="ICommandRouter" />.
    /// </summary>
    [Export(typeof(ICommandRouter))]
    public class CommandRouter : ICommandRouter
    {
        private static readonly Type CommandHandlerInterfaceType = typeof(ICommandHandler<>);
        private static readonly Type CommandListHandlerInterfaceType = typeof(ICommandListHandler<>);
        private readonly Dictionary<Type, HashSet<Type>> _commandHandlerTypeToCommandDefinitionTypesLookup;

        private readonly Dictionary<Type, CommandHandlerWrapper> _globalCommandHandlerWrappers;

        /// <summary>
        ///     Creates a new <see cref="CommandRouter" />.
        /// </summary>
        /// <param name="globalCommandHandlers">A list of available <see cref="ICommandHandler" />s.</param>
        [ImportingConstructor]
        public CommandRouter(
            [ImportMany(typeof(ICommandHandler))] ICommandHandler[] globalCommandHandlers)
        {
            _commandHandlerTypeToCommandDefinitionTypesLookup = new Dictionary<Type, HashSet<Type>>();
            _globalCommandHandlerWrappers = BuildCommandHandlerWrappers(globalCommandHandlers);
        }

        /// <summary>
        ///     Returns the <see cref="CommandHandlerWrapper" /> for invoking an underlying command handler
        ///     associated with the specified <see cref="CommandDefinitionBase" />.
        /// </summary>
        /// <param name="commandDefinition">The <see cref="CommandDefinitionBase" /> for lookup up the command handler.</param>
        /// <returns>A <see cref="CommandHandlerWrapper" /> instance, or null if not available.</returns>
        public CommandHandlerWrapper GetCommandHandler(CommandDefinitionBase commandDefinition)
        {
            CommandHandlerWrapper commandHandler;

            var shell = IoC.Get<IShell>();

            var activeItemViewModel = shell.ActivePanel;
            if (activeItemViewModel != null)
            {
                commandHandler = GetCommandHandlerForLayoutItem(commandDefinition, activeItemViewModel);
                if (commandHandler != null)
                    return commandHandler;
            }

            var activeDocumentViewModel = shell.SelectedDocument;
            if ((activeDocumentViewModel == null) || Equals(activeDocumentViewModel, activeItemViewModel))
                return !_globalCommandHandlerWrappers.TryGetValue(commandDefinition.GetType(), out commandHandler)
                    ? null
                    : commandHandler;
            commandHandler = GetCommandHandlerForLayoutItem(commandDefinition, activeDocumentViewModel);
            if (commandHandler != null)
                return commandHandler;

            // If none of the objects in the DataContext hierarchy handle the command,
            // fallback to the global handler.
            return !_globalCommandHandlerWrappers.TryGetValue(commandDefinition.GetType(), out commandHandler)
                ? null
                : commandHandler;
        }

        private Dictionary<Type, CommandHandlerWrapper> BuildCommandHandlerWrappers(ICommandHandler[] commandHandlers)
        {
            var commandHandlersList = SortCommandHandlers(commandHandlers);

            // Command handlers are either ICommandHandler<T> or ICommandListHandler<T>.
            // We need to extract T, and use it as the key in our dictionary.

            var result = new Dictionary<Type, CommandHandlerWrapper>();

            foreach (var commandHandler in commandHandlersList)
            {
                var commandHandlerType = commandHandler.GetType();
                EnsureCommandHandlerTypeToCommandDefinitionTypesPopulated(commandHandlerType);
                var commandDefinitionTypes = _commandHandlerTypeToCommandDefinitionTypesLookup[commandHandlerType];
                foreach (var commandDefinitionType in commandDefinitionTypes)
                    result[commandDefinitionType] = CreateCommandHandlerWrapper(commandDefinitionType, commandHandler);
            }

            return result;
        }

        private static IEnumerable<ICommandHandler> SortCommandHandlers(IEnumerable<ICommandHandler> commandHandlers)
        {
            // Put command handlers defined in priority assemblies, last. This allows applications
            // to override built-in command handlers.

            var bootstrapper = IoC.Get<AppBootstrapper>();

            return commandHandlers
                .OrderBy(h => bootstrapper.PriorityAssemblies.Contains(h.GetType().Assembly) ? 1 : 0)
                .ToList();
        }

        private CommandHandlerWrapper GetCommandHandlerForLayoutItem(CommandDefinitionBase commandDefinition,
            object activeItemViewModel)
        {
            var activeItemView = ViewLocator.LocateForModel(activeItemViewModel, null, null);
            var activeItemWindow = Window.GetWindow(activeItemView);
            if (activeItemWindow == null)
                return null;

            var startElement = FocusManager.GetFocusedElement(activeItemView) ?? activeItemView;

            // First, we look at the currently focused element, and iterate up through
            // the tree, giving each DataContext a chance to handle the command.
            return FindCommandHandlerInVisualTree(commandDefinition, startElement);
        }

        private CommandHandlerWrapper FindCommandHandlerInVisualTree(CommandDefinitionBase commandDefinition,
            IInputElement target)
        {
            var visualObject = target as DependencyObject;
            if (visualObject == null)
                return null;

            object previousDataContext = null;
            do
            {
                var frameworkElement = visualObject as FrameworkElement;
                var dataContext = frameworkElement?.DataContext;
                if ((dataContext != null) && !ReferenceEquals(dataContext, previousDataContext))
                {
                    var context = dataContext as ICommandRerouter;
                    if (context != null)
                    {
                        var commandRerouter = context;
                        var commandTarget = commandRerouter.GetHandler(commandDefinition);
                        if (commandTarget != null)
                        {
                            if (IsCommandHandlerForCommandDefinitionType(commandTarget, commandDefinition.GetType()))
                                return CreateCommandHandlerWrapper(commandDefinition.GetType(), commandTarget);
                            throw new InvalidOperationException(
                                "This object does not handle the specified command definition.");
                        }
                    }

                    if (IsCommandHandlerForCommandDefinitionType(dataContext, commandDefinition.GetType()))
                        return CreateCommandHandlerWrapper(commandDefinition.GetType(), dataContext);

                    previousDataContext = dataContext;
                }
                visualObject = VisualTreeHelper.GetParent(visualObject);
            } while (visualObject != null);

            return null;
        }

        private static CommandHandlerWrapper CreateCommandHandlerWrapper(
            Type commandDefinitionType, object commandHandler)
        {
            if (typeof(CommandDefinition).IsAssignableFrom(commandDefinitionType))
                return
                    CommandHandlerWrapper.FromCommandHandler(
                        CommandHandlerInterfaceType.MakeGenericType(commandDefinitionType), commandHandler);
            if (typeof(CommandListDefinition).IsAssignableFrom(commandDefinitionType))
                return
                    CommandHandlerWrapper.FromCommandListHandler(
                        CommandListHandlerInterfaceType.MakeGenericType(commandDefinitionType), commandHandler);
            throw new InvalidOperationException();
        }

        private bool IsCommandHandlerForCommandDefinitionType(
            object commandHandler, Type commandDefinitionType)
        {
            var commandHandlerType = commandHandler.GetType();
            EnsureCommandHandlerTypeToCommandDefinitionTypesPopulated(commandHandlerType);
            var commandDefinitionTypes = _commandHandlerTypeToCommandDefinitionTypesLookup[commandHandlerType];
            return commandDefinitionTypes.Contains(commandDefinitionType);
        }

        private void EnsureCommandHandlerTypeToCommandDefinitionTypesPopulated(Type commandHandlerType)
        {
            if (!_commandHandlerTypeToCommandDefinitionTypesLookup.ContainsKey(commandHandlerType))
            {
                var commandDefinitionTypes =
                    _commandHandlerTypeToCommandDefinitionTypesLookup[commandHandlerType] = new HashSet<Type>();

                foreach (
                    var handledCommandDefinitionType in
                    GetAllHandledCommandedDefinitionTypes(commandHandlerType, CommandHandlerInterfaceType))
                    commandDefinitionTypes.Add(handledCommandDefinitionType);

                foreach (
                    var handledCommandDefinitionType in
                    GetAllHandledCommandedDefinitionTypes(commandHandlerType, CommandListHandlerInterfaceType))
                    commandDefinitionTypes.Add(handledCommandDefinitionType);
            }
        }

        private static IEnumerable<Type> GetAllHandledCommandedDefinitionTypes(
            Type type, Type genericInterfaceType)
        {
            var result = new List<Type>();

            while (type != null)
            {
                result.AddRange(type.GetInterfaces()
                    .Where(x => x.IsGenericType && (x.GetGenericTypeDefinition() == genericInterfaceType))
                    .Select(x => x.GetGenericArguments().First()));

                type = type.BaseType;
            }

            return result;
        }
    }
}
