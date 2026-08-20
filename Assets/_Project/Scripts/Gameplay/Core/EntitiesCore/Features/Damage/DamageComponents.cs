using _Project.Scripts.Gameplay.Core.Conditions;
using _Project.Scripts.Gameplay.Core.ReactiveVariable;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore.Features.ApplyDamage
{
    public class TakeDamageRequest : IEntityComponent
    {
        public ReactiveEvent<float> Value;
    }

    public class TakeDamageEvent : IEntityComponent
    {
        public ReactiveEvent<float> Value;
    }

    public class CanApplyDamage : IEntityComponent
    {
        public ICompositeCondition Value;
    }
}