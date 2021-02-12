using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Konoma.CrossFit
{
    internal static class PropertyExpressionParser
    {
        public static BindingProperty<T> ParseExpression<T>(Expression expression, object model, bool mustBeObservable)
        {
            var memberExpression = UnwrapExpression(expression, out var conversion);
            var (source, baseProperty) = GetSourceAndBaseProperty(memberExpression, model, mustBeObservable);
            var (getter, setter) = GetPropertyAccessors<T>(memberExpression, source, conversion);

            return new BindingProperty<T>(getter, setter, baseProperty?.Name);
        }

        private static MemberExpression UnwrapExpression(Expression expression, out MethodInfo? conversion)
        {
            if (expression.NodeType == ExpressionType.Convert)
            {
                var convertExpression = (UnaryExpression)expression;
                conversion = convertExpression.Method;
                return UnwrapAndCheck(convertExpression.Operand);
            }

            conversion = null;
            return UnwrapAndCheck(expression);
        }

        private static MemberExpression UnwrapAndCheck(Expression expression)
        {
            if (expression.NodeType != ExpressionType.MemberAccess)
                throw Fail(expression, "Must be a property access");

            return (MemberExpression)expression;
        }

        private static (object, PropertyInfo?) GetSourceAndBaseProperty(
            MemberExpression expression,
            object model,
            bool mustBeObservable)
        {
            PropertyInfo? baseProperty = null;
            MemberExpression current = expression;
            Func<object, object> currentSourceAccessor = o => o;

            while (current.Expression is MemberExpression innerExpression)
            {
                baseProperty = innerExpression.Member as PropertyInfo;
                var previousAccessor = currentSourceAccessor;

                switch (innerExpression.Member)
                {
                    case PropertyInfo info:
                        currentSourceAccessor = o => info.GetValue(previousAccessor(o));
                        break;

                    case FieldInfo info:
                        currentSourceAccessor = o => info.GetValue(previousAccessor(o));
                        break;

                    default:
                        throw Fail(expression, $"Unsupported inner expression: '{innerExpression}'");
                }

                current = innerExpression;
            }

            if (current.Expression.NodeType != ExpressionType.Parameter)
                throw Fail(expression, $"Unsupported inner expression: '{current.Expression}'");

            if (mustBeObservable && baseProperty is null)
                throw Fail(expression, "Expression must start with an observable property");

            return (currentSourceAccessor(model), baseProperty);
        }

        private static (Func<T>, Action<T>?) GetPropertyAccessors<T>(
            MemberExpression expression,
            object source,
            MethodInfo? conversion)
        {
            if (!(expression.Member is PropertyInfo propertyInfo))
                throw Fail(expression, "Bound value must be a property");

            var setter = propertyInfo.CanWrite && conversion is null
                ? GetSetter()
                : null;

            return (() => (T)propertyInfo.GetValue(source), setter);

            Action<T> GetSetter() => value => propertyInfo.SetValue(source, value);
        }

        private static Exception Fail(Expression expression, string message)
        {
            return new InvalidOperationException($"Cannot bind expression '{expression}': {message}");
        }
    }

    public class BindingProperty<T>
    {
        public BindingProperty(Func<T> getter, Action<T>? setter, string? observablePropertyName)
        {
            Getter = getter;
            Setter = setter;
            ObservablePropertyName = observablePropertyName!;
        }

        public Func<T> Getter { get; }

        public Action<T>? Setter { get; }

        public string ObservablePropertyName { get; }
    }
}
