﻿#region

using System;
using Caliburn.Micro;

#endregion

namespace Gemini.Modules.UndoRedo
{
    public interface IUndoRedoManager
    {
        IObservableCollection<IUndoableAction> UndoStack { get; }
        IObservableCollection<IUndoableAction> RedoStack { get; }

        int? UndoCountLimit { get; set; }

        event EventHandler BatchBegin;
        event EventHandler BatchEnd;

        void ExecuteAction(IUndoableAction action);

        void Undo(int actionCount);
        void UndoTo(IUndoableAction action);
        void UndoAll();

        void Redo(int actionCount);
        void RedoTo(IUndoableAction action);
    }
}
