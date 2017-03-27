#region

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

#endregion

namespace Gemini.Framework.Commands
{
    /// <summary>
    ///     Wraps a generic <see cref="ICommandHandler{TCommandDefinition}" /> or
    ///     <see cref="ICommandListHandler{TCommandListDefinition}" />
    ///     and allows easy calling of generic interface methods.
    /// </summary>
    public sealed class CommandHandlerWrapper
    {
        private readonly object _commandHandler;
        private readonly MethodInfo _populateMethod;
        private readonly MethodInfo _runMethod;
        private readonly MethodInfo _updateMethod;

        private CommandHandlerWrapper(
            object commandHandler,
            MethodInfo updateMethod,
            MethodInfo populateMethod,
            MethodInfo runMethod)
        {
            _commandHandler = commandHandler;
            _updateMethod = updateMethod;
            _populateMethod = populateMethod;
            _runMethod = runMethod;
        }

        /// <summary>
        ///     Creates a new <see cref="CommandHandlerWrapper" /> for the specified command handler type contract.
        /// </summary>
        /// <param name="commandHandlerInterfaceType">The type contract of the command handler.</param>
        /// <param name="commandHandler">The command handler instance.</param>
        /// <returns>A <see cref="CommandHandlerWrapper" /> representing the command handler.</returns>
        /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name. </exception>
        public static CommandHandlerWrapper FromCommandHandler(Type commandHandlerInterfaceType, object commandHandler)
        {
            var updateMethod = commandHandlerInterfaceType.GetMethod("Update");
            var runMethod = commandHandlerInterfaceType.GetMethod("Run");
            return new CommandHandlerWrapper(commandHandler, updateMethod, null, runMethod);
        }

        /// <summary>
        ///     Creates a new <see cref="CommandHandlerWrapper" /> for the specified command list handler type contract.
        /// </summary>
        /// <param name="commandHandlerInterfaceType">The type contract of the command handler.</param>
        /// <param name="commandListHandler">The command list handler instance.</param>
        /// <returns>A <see cref="CommandHandlerWrapper" /> representing the command list handler.</returns>
        /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name. </exception>
        public static CommandHandlerWrapper FromCommandListHandler(Type commandHandlerInterfaceType,
            object commandListHandler)
        {
            var populateMethod = commandHandlerInterfaceType.GetMethod("Populate");
            var runMethod = commandHandlerInterfaceType.GetMethod("Run");
            return new CommandHandlerWrapper(commandListHandler, null, populateMethod, runMethod);
        }

        /// <summary>
        ///     Invokes the Update method of the underlying command handler.
        /// </summary>
        /// <param name="command">The <see cref="Command" /> argument.</param>
        /// <exception cref="TargetException">
        ///     In the .NET for Windows Store apps or the Portable Class Library, catch
        ///     <see cref="T:System.Exception" /> instead.The parameter is null and the method is not static.-or- The method is not
        ///     declared or inherited by the class of the parameter. -or-A static constructor is invoked, and the object is neither
        ///     null nor an instance of the class that declared the constructor.
        /// </exception>
        /// <exception cref="TargetInvocationException">
        ///     The invoked method or constructor throws an exception. -or-The current
        ///     instance is a <see cref="T:System.Reflection.Emit.DynamicMethod" /> that contains unverifiable code. See the
        ///     "Verification" section in Remarks for <see cref="T:System.Reflection.Emit.DynamicMethod" />.
        /// </exception>
        /// <exception cref="TargetParameterCountException">The parameters array does not have the correct number of arguments. </exception>
        /// <exception cref="MethodAccessException">
        ///     In the .NET for Windows Store apps or the Portable Class Library, catch the
        ///     base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to
        ///     execute the method or constructor that is represented by the current instance.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The elements of the parameters array do not match the signature of the method or
        ///     constructor reflected by this instance.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type that declares the method is an open generic type. That is, the
        ///     <see cref="P:System.Type.ContainsGenericParameters" /> property returns true for the declaring type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The current instance is a <see cref="T:System.Reflection.Emit.MethodBuilder" />
        ///     .
        /// </exception>
        public void Update(Command command)
        {
            if (_updateMethod != null)
                _updateMethod.Invoke(_commandHandler, new object[] {command});
        }

        /// <summary>
        ///     Invokes the Populate method of the underlying command handler.
        /// </summary>
        /// <param name="command">The <see cref="Command" /> argument.</param>
        /// <param name="commands">A list of <see cref="Command" />s to populate.</param>
        /// <exception cref="TargetException">
        ///     In the .NET for Windows Store apps or the Portable Class Library, catch
        ///     <see cref="T:System.Exception" /> instead.The parameter is null and the method is not static.-or- The method is not
        ///     declared or inherited by the class of the parameter. -or-A static constructor is invoked, and the object is neither
        ///     null nor an instance of the class that declared the constructor.
        /// </exception>
        /// <exception cref="TargetInvocationException">
        ///     The invoked method or constructor throws an exception. -or-The current
        ///     instance is a <see cref="T:System.Reflection.Emit.DynamicMethod" /> that contains unverifiable code. See the
        ///     "Verification" section in Remarks for <see cref="T:System.Reflection.Emit.DynamicMethod" />.
        /// </exception>
        /// <exception cref="TargetParameterCountException">The parameters array does not have the correct number of arguments. </exception>
        /// <exception cref="MethodAccessException">
        ///     In the .NET for Windows Store apps or the Portable Class Library, catch the
        ///     base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to
        ///     execute the method or constructor that is represented by the current instance.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The elements of the parameters array do not match the signature of the method or
        ///     constructor reflected by this instance.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type that declares the method is an open generic type. That is, the
        ///     <see cref="P:System.Type.ContainsGenericParameters" /> property returns true for the declaring type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The current instance is a <see cref="T:System.Reflection.Emit.MethodBuilder" />
        ///     .
        /// </exception>
        public void Populate(Command command, List<Command> commands)
        {
            if (_populateMethod == null)
                throw new InvalidOperationException("Populate can only be called for list-type commands.");
            _populateMethod.Invoke(_commandHandler, new object[] {command, commands});
        }

        /// <summary>
        ///     Invokes the Run method of the underlying command handler.
        /// </summary>
        /// <param name="command">The <see cref="Command" /> argument.</param>
        /// <exception cref="TargetException">
        ///     In the .NET for Windows Store apps or the Portable Class Library, catch
        ///     <see cref="T:System.Exception" /> instead.The parameter is null and the method is not static.-or- The method is not
        ///     declared or inherited by the class of the parameter. -or-A static constructor is invoked, and the object is neither
        ///     null nor an instance of the class that declared the constructor.
        /// </exception>
        /// <exception cref="TargetInvocationException">
        ///     The invoked method or constructor throws an exception. -or-The current
        ///     instance is a <see cref="T:System.Reflection.Emit.DynamicMethod" /> that contains unverifiable code. See the
        ///     "Verification" section in Remarks for <see cref="T:System.Reflection.Emit.DynamicMethod" />.
        /// </exception>
        /// <exception cref="TargetParameterCountException">The parameters array does not have the correct number of arguments. </exception>
        /// <exception cref="MethodAccessException">
        ///     In the .NET for Windows Store apps or the Portable Class Library, catch the
        ///     base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to
        ///     execute the method or constructor that is represented by the current instance.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The elements of the parameters array do not match the signature of the method or
        ///     constructor reflected by this instance.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type that declares the method is an open generic type. That is, the
        ///     <see cref="P:System.Type.ContainsGenericParameters" /> property returns true for the declaring type.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The current instance is a <see cref="T:System.Reflection.Emit.MethodBuilder" />
        ///     .
        /// </exception>
        public Task Run(Command command) => (Task) _runMethod.Invoke(_commandHandler, new object[] {command});
    }
}
