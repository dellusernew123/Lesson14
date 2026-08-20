using System;

namespace _Project.Scripts.Gameplay.Core.ReactiveVariable
{
    public interface IReadOnlyVariable<T>
    {
        T Value { get; }

        IDisposable Subscribe(Action<T, T> action);
    }
}