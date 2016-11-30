namespace Gemini.Modules.Inspector.Inspectors
{
    public class RangeEditorViewModel<T> : SelectiveUndoEditorBase<T>, ILabelledInspector
    {
        public RangeEditorViewModel(T minimum, T maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public T Minimum { get; }

        public T Maximum { get; }

        public void DragStarted()
        {
            OnBeginEdit();
        }

        public void DragCompleted()
        {
            OnEndEdit();
        }
    }
}