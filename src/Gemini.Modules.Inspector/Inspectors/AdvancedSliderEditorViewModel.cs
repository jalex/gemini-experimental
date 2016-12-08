#region

using System;
using Caliburn.Micro;
using Gemini.Framework.Controls;
using Gemini.Framework.Util;

#endregion

namespace Gemini.Modules.Inspector.Inspectors
{
    public class AdvancedSliderEditorViewModel<TValue> : SelectiveUndoEditorBase<TValue>, ILabelledInspector, IViewAware
        where TValue : IComparable<TValue>
    {
        private static readonly string DefaultValueFormat = "{0:0.#####}";

        private TValue _maximum;

        private TValue _minimum;

        private bool _mouseCaptured;

        private TValue _speed;

        private AdvancedSliderBase.DisplayType _type;

        private string _valueFormat;

        private Type _valueType;

        private AdvancedSliderEditorView _view;

        public TValue Minimum
        {
            get { return _minimum; }
            set
            {
                _minimum = value;
                NotifyOfPropertyChange(() => Minimum);
            }
        }

        public TValue Maximum
        {
            get { return _maximum; }
            set
            {
                _maximum = value;
                NotifyOfPropertyChange(() => Maximum);
            }
        }

        public TValue Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                NotifyOfPropertyChange(() => Speed);
            }
        }

        public bool MouseCaptured
        {
            get { return _mouseCaptured; }
            set
            {
                if (_mouseCaptured == value)
                    return;

                _mouseCaptured = value;

                if (value)
                    OnBeginEdit();
                else
                    OnEndEdit();
            }
        }

        public string ValueFormat
        {
            get { return _valueFormat; }
            set
            {
                _valueFormat = value;
                NotifyOfPropertyChange(() => ValueFormat);
            }
        }

        public Type ValueType
        {
            get { return _valueType; }
            set
            {
                _valueType = value;
                NotifyOfPropertyChange(() => ValueType);
            }
        }

        public AdvancedSliderBase.DisplayType Type
        {
            get { return _type; }

            set
            {
                if (Equals(_type, value))
                    return;

                _type = value;

                NotifyOfPropertyChange(() => Type);
            }
        }

        public AdvancedSliderEditorViewModel()
        {
            _valueFormat = DefaultValueFormat;
            _valueType = typeof(TValue);
            _type = AdvancedSliderBase.DisplayType.Number;
        }

        public AdvancedSliderEditorViewModel(TValue min, TValue max)
        {
            _minimum = min;
            _maximum = max;
            _valueFormat = DefaultValueFormat;
            _valueType = typeof(TValue);
            _type = AdvancedSliderBase.DisplayType.Bar;
        }

        public event EventHandler<ViewAttachedEventArgs> ViewAttached;

        public void AttachView(object view, object context = null)
        {
            _view = (AdvancedSliderEditorView) view;
            ViewAttached?.Invoke(this, new ViewAttachedEventArgs {View = view, Context = context});
        }

        public object GetView(object context = null)
        {
            return _view;
        }

        public void Up()
        {
            _view.Slider.ApplyValueChange(SpeedMultiplier.Get());
        }

        public void Down()
        {
            _view.Slider.ApplyValueChange(-SpeedMultiplier.Get());
        }
    }
}