using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Gemini.Modules.Inspector.Util
{
    internal static class ExpressionUtility
    {
        public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return GetPropertyNameInternal(expression);
        }

        private static string GetPropertyNameInternal(LambdaExpression expression)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{expression}' refers to a method, not a property.");

            var propertyInfo = member.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException($"Expression '{expression}' refers to a field, not a property.");

            return propertyInfo.Name;
        }
    }
}