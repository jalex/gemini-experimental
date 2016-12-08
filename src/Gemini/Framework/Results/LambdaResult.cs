#region

using System;
using Caliburn.Micro;

#endregion

namespace Gemini.Framework.Results
{
    public class LambdaResult : IResult
    {
        private readonly Action<CoroutineExecutionContext> _lambda;

        public LambdaResult(Action<CoroutineExecutionContext> lambda)
        {
            _lambda = lambda;
        }

        public void Execute(CoroutineExecutionContext context)
        {
            _lambda(context);

            var completedHandler = Completed;
            completedHandler?.Invoke(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;
    }
}