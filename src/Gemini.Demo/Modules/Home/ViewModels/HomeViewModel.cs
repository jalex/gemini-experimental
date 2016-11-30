#region

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Gemini.Framework;

#endregion

namespace Gemini.Demo.Modules.Home.ViewModels
{
    [DisplayName("Home View Model")]
    [Export]
    public class HomeViewModel : Document
    {
        private Color _background;

        private double _doubleValue;

        private float _floatValue;

        private Color _foreground;

        private int _integerValue;

        private double? _nullableDoubleValue;

        private float? _nullableFloatValue;

        private int? _nullableIntegerValue;

        private string _text;

        private TextAlignment _textAlignment;

        public HomeViewModel()
        {
            DisplayName = "Home";
            Background = Colors.CornflowerBlue;
            Foreground = Colors.White;
            TextAlignment = TextAlignment.Center;
            Text = "Welcome to the Gemini Demo!";
            IntegerValue = 3;
            NullableIntegerValue = null;
            FloatValue = 5.2f;
            NullableFloatValue = null;
            DoubleValue = Math.PI;
            NullableDoubleValue = 4.5;
        }

        public Color Background
        {
            get { return _background; }
            set
            {
                _background = value;
                NotifyOfPropertyChange(() => Background);
            }
        }

        public Color Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                NotifyOfPropertyChange(() => Foreground);
            }
        }

        [DisplayName("Text Alignment")]
        public TextAlignment TextAlignment
        {
            get { return _textAlignment; }
            set
            {
                _textAlignment = value;
                NotifyOfPropertyChange(() => TextAlignment);
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        [DisplayName("Integer Value")]
        public int IntegerValue
        {
            get { return _integerValue; }
            set
            {
                _integerValue = value;
                NotifyOfPropertyChange(() => IntegerValue);
            }
        }

        [DisplayName("Nullable Integer Value")]
        public int? NullableIntegerValue
        {
            get { return _nullableIntegerValue; }
            set
            {
                _nullableIntegerValue = value;
                NotifyOfPropertyChange(() => NullableIntegerValue);
            }
        }

        [DisplayName("Float Value")]
        public float FloatValue
        {
            get { return _floatValue; }
            set
            {
                _floatValue = value;
                NotifyOfPropertyChange(() => FloatValue);
            }
        }

        [DisplayName("Nullable Float Value")]
        public float? NullableFloatValue
        {
            get { return _nullableFloatValue; }
            set
            {
                _nullableFloatValue = value;
                NotifyOfPropertyChange(() => NullableFloatValue);
            }
        }

        [DisplayName("Double Value")]
        public double DoubleValue
        {
            get { return _doubleValue; }
            set
            {
                _doubleValue = value;
                NotifyOfPropertyChange(() => DoubleValue);
            }
        }

        [DisplayName("Nullable Double Value")]
        public double? NullableDoubleValue
        {
            get { return _nullableDoubleValue; }
            set
            {
                _nullableDoubleValue = value;
                NotifyOfPropertyChange(() => NullableDoubleValue);
            }
        }

        public override bool ShouldReopenOnStart => true;

        public override void SaveState(BinaryWriter writer)
        {
            // save color as byte information
            writer.Write(Background.A);
            writer.Write(Background.R);
            writer.Write(Background.G);
            writer.Write(Background.B);

            // save foreground
            writer.Write(Foreground.A);
            writer.Write(Foreground.R);
            writer.Write(Foreground.G);
            writer.Write(Foreground.B);

            // save TextAlignment as a string
            writer.Write(TextAlignment.ToString());
        }

        public override void LoadState(BinaryReader reader)
        {
            // load color
            Background = new Color
            {
                A = reader.ReadByte(),
                R = reader.ReadByte(),
                G = reader.ReadByte(),
                B = reader.ReadByte()
            };
            Foreground = new Color
            {
                A = reader.ReadByte(),
                R = reader.ReadByte(),
                G = reader.ReadByte(),
                B = reader.ReadByte()
            };

            // load TextAlignment as a string
            TextAlignment = (TextAlignment) Enum.Parse(typeof(TextAlignment), reader.ReadString());
        }
    }
}