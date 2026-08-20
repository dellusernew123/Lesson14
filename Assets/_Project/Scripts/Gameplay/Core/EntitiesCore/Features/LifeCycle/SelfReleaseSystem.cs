using _Project.Scripts.Gameplay.Core.ReactiveVariable;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore.Features.LifeCycle
{
    public class SelfReleaseSystem : IInitializableSystem, IUpdatableSystem
    {
        private readonly EntitiesLifeContext _entitiesLifeContext;

        private Entity _entity;

        private ReactiveVariable<bool> _isDead;

        public SelfReleaseSystem(EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesLifeContext = entitiesLifeContext;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _isDead = entity.IsDead;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value == true)
                _entitiesLifeContext.Release(_entity);
        }
    }
}