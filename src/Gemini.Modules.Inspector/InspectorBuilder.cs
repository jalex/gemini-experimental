﻿#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Gemini.Modules.Inspector.Conventions;
using Gemini.Modules.Inspector.Inspectors;
using Gemini.Modules.Inspector.Properties;
using Gemini.Modules.Inspector.Util;

#endregion

namespace Gemini.Modules.Inspector
{
    public class InspectorBuilder<TBuilder>
        where TBuilder : InspectorBuilder<TBuilder>
    {
        protected List<IInspector> Inspectors { get; }

        public InspectorBuilder()
        {
            Inspectors = new List<IInspector>();
        }

        public TBuilder WithCollapsibleGroup(string name,
            Func<CollapsibleGroupBuilder, CollapsibleGroupBuilder> callback)
        {
            var builder = new CollapsibleGroupBuilder();
            Inspectors.Add(callback(builder).ToCollapsibleGroup(name));
            return (TBuilder) this;
        }

        public TBuilder WithCheckBoxEditor<T>(T instance, Expression<Func<T, bool>> propertyExpression)
        {
            return WithEditor<T, bool, CheckBoxEditorViewModel>(instance, propertyExpression);
        }

        public TBuilder WithColorEditor<T>(T instance, Expression<Func<T, Color>> propertyExpression)
        {
            return WithEditor<T, Color, ColorEditorViewModel>(instance, propertyExpression);
        }

        public TBuilder WithEnumEditor<T, TProperty>(T instance, Expression<Func<T, TProperty>> propertyExpression)
        {
            return WithEditor<T, TProperty, EnumEditorViewModel<TProperty>>(instance, propertyExpression);
        }

        public TBuilder WithPoint3DEditor<T>(T instance, Expression<Func<T, Point3D>> propertyExpression)
        {
            return WithEditor<T, Point3D, Point3DEditorViewModel>(instance, propertyExpression);
        }

        public TBuilder WithRangeEditor<T>(T instance, Expression<Func<T, float>> propertyExpression, float minimum,
            float maximum)
        {
            return WithEditor(instance, propertyExpression, new RangeEditorViewModel<float>(minimum, maximum));
        }

        public TBuilder WithRangeEditor<T>(T instance, Expression<Func<T, double>> propertyExpression, double minimum,
            double maximum)
        {
            return WithEditor(instance, propertyExpression, new RangeEditorViewModel<double>(minimum, maximum));
        }

        public TBuilder WithEditor<T, TProperty, TEditor>(T instance, Expression<Func<T, TProperty>> propertyExpression)
            where TEditor : IEditor, new()
        {
            return WithEditor(instance, propertyExpression, new TEditor());
        }

        public TBuilder WithEditor<T, TProperty, TEditor>(T instance, Expression<Func<T, TProperty>> propertyExpression,
            TEditor editor)
            where TEditor : IEditor
        {
            var propertyName = ExpressionUtility.GetPropertyName(propertyExpression);
            editor.BoundPropertyDescriptor = BoundPropertyDescriptor.FromProperty(instance, propertyName);
            Inspectors.Add(editor);
            return (TBuilder) this;
        }

        public TBuilder WithObjectProperties(object instance, Func<PropertyDescriptor, bool> propertyFilter)
        {
            var properties = TypeDescriptor.GetProperties(instance)
                .Cast<PropertyDescriptor>()
                .Where(x => x.IsBrowsable && propertyFilter(x))
                .ToList();

            // If any properties are not in the default group, show all properties in collapsible groups.
            if (
                properties.Any(
                    x => !string.IsNullOrEmpty(x.Category) && x.Category != CategoryAttribute.Default.Category))
                foreach (var category in properties.GroupBy(x => x.Category))
                {
                    var actualCategory = string.IsNullOrEmpty(category.Key) ||
                                         category.Key == CategoryAttribute.Default.Category
                        ? Resources.InspectorBuilderMiscellaneous
                        : category.Key;

                    var collapsibleGroupBuilder = new CollapsibleGroupBuilder();
                    AddProperties(instance, category, collapsibleGroupBuilder.Inspectors);
                    if (collapsibleGroupBuilder.Inspectors.Any())
                        Inspectors.Add(collapsibleGroupBuilder.ToCollapsibleGroup(actualCategory));
                }
            else // Otherwise, show properties in flat list.
                AddProperties(instance, properties, Inspectors);

            return (TBuilder) this;
        }

        public TBuilder WithObjectProperty(object instance, PropertyDescriptor property)
        {
            var editor = DefaultPropertyInspectors.CreateEditor(property);
            if (editor != null)
            {
                editor.BoundPropertyDescriptor = new BoundPropertyDescriptor(instance, property);
                Inspectors.Add(editor);
            }

            return (TBuilder) this;
        }

        private static void AddProperties(object instance, IEnumerable<PropertyDescriptor> properties,
            List<IInspector> inspectors)
        {
            foreach (var property in properties)
            {
                var editor = DefaultPropertyInspectors.CreateEditor(property);
                if (editor != null)
                {
                    editor.BoundPropertyDescriptor = new BoundPropertyDescriptor(instance, property);
                    inspectors.Add(editor);
                }
            }
        }
    }
}
