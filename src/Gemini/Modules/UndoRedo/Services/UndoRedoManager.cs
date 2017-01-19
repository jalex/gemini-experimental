#region

using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;

#endregion

namespace Gemini.Modules.UndoRedo.Services
{
    public class UndoRedoManager : IUndoRedoManager
    {
        private readonly BindableCollection<IUndoableAction> _redoStack;
        private readonly BindableCollection<IUndoableAction> _undoStack;

        private int? _undoCountLimit;

        public IObservableCollection<IUndoableAction> UndoStack => _undoStack;

        public IObservableCollection<IUndoableAction> RedoStack => _redoStack;

        public int? UndoCountLimit
        {
            get { return _undoCountLimit; }

            set
            {
                _undoCountLimit = value;
                EnforceLimit();
            }
        }

        public UndoRedoManager()
        {
            _undoStack = new BindableCollection<IUndoableAction>();
            _redoStack = new BindableCollection<IUndoableAction>();
        }

        public event EventHandler BatchBegin;
        public event EventHandler BatchEnd;

        public virtual void ExecuteAction(IUndoableAction action)
        {
            action.Execute();
            Push(_undoStack, action);
            _redoStack.Clear();
            EnforceLimit();
        }

        public void Undo(int actionCount)
        {
            OnBegin();

            try
            {
                for (var i = 0; i < actionCount; i++)
                {
                    var action = Pop(_undoStack);
                    action.Undo();
                    Push(_redoStack, action);
                }
            }
            finally
            {
                OnEnd();
            }
        }

        public void UndoTo(IUndoableAction action)
        {
            OnBegin();

            try
            {
                while (true)
                {
                    if (Peek(_undoStack) == action)
                        return;
                    var thisAction = Pop(_undoStack);
                    thisAction.Undo();
                    Push(_redoStack, thisAction);
                }
            }
            finally
            {
                OnEnd();
            }
        }

        public void UndoAll()
        {
            Undo(_undoStack.Count);
        }

        public void Redo(int actionCount)
        {
            OnBegin();

            try
            {
                for (var i = 0; i < actionCount; i++)
                {
                    var action = Pop(_redoStack);
                    action.Execute();
                    Push(_undoStack, action);
                }

                EnforceLimit();
            }
            finally
            {
                OnEnd();
            }
        }

        public void RedoTo(IUndoableAction action)
        {
            OnBegin();

            try
            {
                while (true)
                {
                    var thisAction = Pop(_redoStack);
                    thisAction.Execute();
                    Push(_undoStack, thisAction);
                    if (thisAction == action)
                        break;
                }

                EnforceLimit();
            }
            finally
            {
                OnEnd();
            }
        }

        protected void EnforceLimit()
        {
            if (!_undoCountLimit.HasValue)
                return;

            while (_undoStack.Count > UndoCountLimit.Value)
                PopFront(_undoStack);
        }

        private void OnBegin()
        {
            var handler = BatchBegin;
            handler?.Invoke(this, EventArgs.Empty);
        }

        private void OnEnd()
        {
            var handler = BatchEnd;
            handler?.Invoke(this, EventArgs.Empty);
        }

        protected static IUndoableAction Peek(IObservableCollection<IUndoableAction> stack)
        {
            return stack[stack.Count - 1];
        }

        protected static void Push(IObservableCollection<IUndoableAction> stack, IUndoableAction action)
        {
            stack.Add(action);
        }

        protected static IUndoableAction Pop(IObservableCollection<IUndoableAction> stack)
        {
            var item = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);
            return item;
        }

        protected static void PopFront(IObservableCollection<IUndoableAction> stack)
        {
            var item = stack[0];
            stack.RemoveAt(0);
        }
    }
}