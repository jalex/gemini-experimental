#region

using System;
using Caliburn.Micro;

#endregion

namespace Gemini.Framework.Results
{
    public abstract class OpenResultBase<TTarget> : IOpenResult<TTarget>
    {
        protected Action<TTarget> _onConfigure;
        protected Action<TTarget> _onShutDown;
        protected Action<TTarget> SetData;

        Action<TTarget> IOpenResult<TTarget>.OnConfigure
        {
            get { return _onConfigure; }
            set { _onConfigure = value; }
        }

        Action<TTarget> IOpenResult<TTarget>.OnShutDown
        {
            get { return _onShutDown; }
            set { _onShutDown = value; }
        }

        public abstract void Execute(CoroutineExecutionContext context);

        public event EventHandler<ResultCompletionEventArgs> Completed;

        //void IOpenResult<TTarget>.SetData<TData>(TData data)
        //{
        //    _setData = child =>
        //    {
        //        var dataCentric = (IDataCentric<TData>)child;
        //        dataCentric.LoadData(data);
        //    };
        //}

        protected virtual void OnCompleted(Exception exception, bool wasCancelled)
        {
            Completed?.Invoke(this, new ResultCompletionEventArgs {Error = exception, WasCancelled = wasCancelled});
        }
    }
}
