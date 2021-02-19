using System;
using System.Windows.Input;

namespace Konoma.CrossFit
{
    public class RedirectCommand<T> : CommandBase<T>
    {
        public static RedirectCommand<T> From<TTarget>(ICommand<TTarget> target, Func<T, TTarget> transform) =>
            new RedirectCommand<T>(target, p => transform(p));

        protected RedirectCommand(ICommand targetCommand, Func<T, object?>? parameterTransform)
        {
            _targetCommand = targetCommand;
            _parameterTransform = parameterTransform;
        }

        private readonly ICommand _targetCommand;
        private readonly Func<T, object?>? _parameterTransform;

        public override bool CanExecute(T parameter) => _targetCommand.CanExecute(Transform(parameter));

        public override void Execute(T parameter) => _targetCommand.Execute(Transform(parameter));

#pragma warning disable 8601
        // ReSharper disable once ReturnTypeCanBeNotNullable
        private object? Transform(T parameter) => _parameterTransform?.Invoke(parameter) ?? parameter;
#pragma warning restore 8601
    }

    public class RedirectCommand : RedirectCommand<object?>
    {
        public static new RedirectCommand From<TTarget>(ICommand<TTarget> target, Func<object?, TTarget> transform) =>
            new RedirectCommand(target, p => transform(p));

        protected RedirectCommand(ICommand targetCommand, Func<object?, object?>? parameterTransform) : base(targetCommand, parameterTransform)
        {
        }
    }
}
