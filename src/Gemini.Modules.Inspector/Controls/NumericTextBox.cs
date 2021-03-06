﻿#region

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#endregion

namespace Gemini.Modules.Inspector.Controls
{
    public class NumericTextBox : Control
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(NumericTextBox),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null,
                OnCoerceValue));

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum", typeof(double?), typeof(NumericTextBox));

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", typeof(double?), typeof(NumericTextBox));

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
            "Mode", typeof(NumericTextBoxMode), typeof(NumericTextBox),
            new FrameworkPropertyMetadata(NumericTextBoxMode.Normal));

        private TextBlock _textBlock;
        private TextBox _textBox;

        public double Value
        {
            get { return (double) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public double? Minimum
        {
            get { return (double?) GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public double? Maximum
        {
            get { return (double?) GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public NumericTextBoxMode Mode
        {
            get { return (NumericTextBoxMode) GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        static NumericTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericTextBox),
                new FrameworkPropertyMetadata(typeof(NumericTextBox)));
        }

        private static object OnCoerceValue(DependencyObject d, object basevalue)
        {
            return ((NumericTextBox) d).CoerceValue((double) basevalue);
        }

        public override void OnApplyTemplate()
        {
            _textBlock = (TextBlock) Template.FindName("TextBlock", this);

            var originalPosition = new Point();
            double originalValue = 0;
            var mouseMoved = false;

            _textBlock.MouseDown += (sender, e) =>
            {
                originalPosition = e.GetPosition(_textBlock);
                originalValue = Value;
                _textBlock.CaptureMouse();
                mouseMoved = false;
            };

            _textBlock.MouseMove += (sender, e) =>
            {
                if (!_textBlock.IsMouseCaptured)
                    return;

                mouseMoved = true;

                var newPosition = e.GetPosition(_textBlock);
                Value = CoerceValue(originalValue + (newPosition.X - originalPosition.X) / 50.0);
            };

            _textBlock.MouseUp += (sender, e) =>
            {
                if (_textBlock.IsMouseCaptured)
                    _textBlock.ReleaseMouseCapture();

                if (!mouseMoved)
                {
                    Mode = NumericTextBoxMode.TextBox;
                    _textBox.SelectAll();
                    _textBox.Focus();
                }
            };

            _textBox = (TextBox) Template.FindName("TextBox", this);
            _textBox.KeyUp += (sender, e) =>
            {
                if (e.Key == Key.Escape || e.Key == Key.Enter)
                    Mode = NumericTextBoxMode.Normal;
            };
            _textBox.LostFocus += (sender, e) => Mode = NumericTextBoxMode.Normal;

            base.OnApplyTemplate();
        }

        private double CoerceValue(double newValue)
        {
            if (Minimum != null && newValue < Minimum.Value)
                return Minimum.Value;
            if (Maximum != null && newValue > Maximum.Value)
                return Maximum.Value;
            return newValue;
        }
    }
}
