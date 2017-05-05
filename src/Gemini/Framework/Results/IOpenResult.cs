#region

using System;
using Caliburn.Micro;

#endregion

namespace Gemini.Framework.Results
{

    /// <summary>
    ///     Represents an <see cref="IResult"/> for opening a child type.
    /// </summary>
    /// <typeparam name="TChild">The type of the child to open.</typeparam>
    public interface IOpenResult<TChild> : IResult
    {

        /// <summary>
        ///     Gets or sets an action delegate which is invoked for configuring an instance of <typeparamref name="TChild"/>.
        /// </summary>
        Action<TChild> OnConfigure { get; set; }

        /// <summary>
        ///     Gets or sets an action delegate which is invoked for shutting down the instance of <typeparamref name="TChild"/>.
        /// </summary>
        Action<TChild> OnShutDown { get; set; }

        //void SetData<TData>(TData data);
    }
}
