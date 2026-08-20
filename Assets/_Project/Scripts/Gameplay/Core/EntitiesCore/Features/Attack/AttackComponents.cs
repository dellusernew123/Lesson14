using _Project.Scripts.Gameplay.Core.ReactiveVariable;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore.Features.Attack
{
    public class AttackRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class AttackDamage : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}