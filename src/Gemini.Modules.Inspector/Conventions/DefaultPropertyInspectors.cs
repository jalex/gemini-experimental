#region

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Gemini.Modules.Inspector.Inspectors;

#endregion

namespace Gemini.Modules.Inspector.Conventions
{
    public static class DefaultPropertyInspectors
    {
        public static List<PropertyEditorBuilder> InspectorBuilders { get; }

        static DefaultPropertyInspectors()
        {
            InspectorBuilders = new List<PropertyEditorBuilder>
            {
                new AdvancedSliderPropertyEditorBuilder(),
                new AdvancedSliderRangePropertyEditorBuilder(),
                new EnumPropertyEditorBuilder(),
                new StandardPropertyEditorBuilder<bool, CheckBoxEditorViewModel>(),
                new StandardPropertyEditorBuilder<Color, ColorEditorViewModel>(),
                new StandardPropertyEditorBuilder<double?, TextBoxEditorViewModel<double?>>(),
                new StandardPropertyEditorBuilder<float?, TextBoxEditorViewModel<float?>>(),
                new StandardPropertyEditorBuilder<int?, TextBoxEditorViewModel<int?>>(),
                new StandardPropertyEditorBuilder<Point3D, Point3DEditorViewModel>(),
                new StandardPropertyEditorBuilder<string, TextBoxEditorViewModel<string>>(),

                new StandardPropertyEditorBuilder<BitmapSource, BitmapSourceEditorViewModel>(),
                new StandardPropertyEditorBuilder<InspectorList, InspectorListEditorViewModel>()
            };
        }

        public static IEditor CreateEditor(PropertyDescriptor propertyDescriptor)
        {
            foreach (var inspectorBuilder in InspectorBuilders)
                if (inspectorBuilder.IsApplicable(propertyDescriptor))
                    return inspectorBuilder.BuildEditor(propertyDescriptor);
            return null;
        }
    }
}
