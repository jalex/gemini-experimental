namespace Gemini.Modules.Inspector.Inspectors
{
    public class RangeEditorViewModel<T> : SelectiveUndoEditorBase<T>, ILabelledInspector
    {
        public T Minimum { get; }

        public T Maximum { get; }

        public RangeEditorViewModel(T minimum, T maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

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
