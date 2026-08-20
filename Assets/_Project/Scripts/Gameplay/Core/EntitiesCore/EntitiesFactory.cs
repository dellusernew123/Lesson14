using _Project.Scripts.Gameplay.Core.Conditions;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Buffer;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Features.ApplyDamage;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Features.Attack;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Features.LifeCycle;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Features.Sensors;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Mono;
using _Project.Scripts.Gameplay.Core.EntitiesCore.MovementFeature.RigidbodySystems.Mover;
using _Project.Scripts.Gameplay.Core.ReactiveVariable;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        private readonly CollidersRegistryService _collidersRegistryService;

        public EntitiesFactory
        (
            EntitiesLifeContext entitiesLifeContext,
            MonoEntitiesFactory monoEntitiesFactory,
            CollidersRegistryService collidersRegistryService
        )
        {
            _entitiesLifeContext = entitiesLifeContext;
            _monoEntitiesFactory = monoEntitiesFactory;
            _collidersRegistryService = collidersRegistryService;
        }

        public Entity CreatePlayer(Vector3 position)
        {
            Entity entity = CreateEntity();

            _monoEntitiesFactory.Create(entity, position, "Prefabs/PlayerRigidbody");

            entity
                .AddEnergy(new ReactiveVariable<float>(100))
                .AddMinEnergyForTeleportation(new ReactiveVariable<float>(50))
                .AddMaxEnergy(new ReactiveVariable<float>(100))
                .AddEnergyPriceForTeleportation(new ReactiveVariable<float>(50))
                .AddEnergyRegenerationTime(new ReactiveVariable<float>(5))
                .AddTeleportationRadius(new ReactiveVariable<float>(10))
                .AddMakeTeleportationEvent(new ReactiveEvent<Vector3>())
                .AddMakeTeleportationRequest(new ReactiveEvent())
                .AddMaxHealth(new ReactiveVariable<float>(100))
                .AddCurrentHealth(new ReactiveVariable<float>(100))
                .AddAttackDamage(new ReactiveVariable<float>(20))
                .AddAttackRadius(new ReactiveVariable<float>(5))
                .AddFindCollidersEvent(new ReactiveEvent())
                .AddIsDead(new ReactiveVariable<bool>(false))
                .AddTakeDamageRequest(new ReactiveEvent<float>())
                .AddTakeDamageEvent(new ReactiveEvent<float>())
                .AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
                .AddContactColliderBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                .AddCanApplyDamage(canApplyDamage);

            entity
                .AddContactsDetectedEvent(new ReactiveEvent())
                .AddSystem(new TeleportationMovementSystem())
                .AddSystem(new BodyContactsDetectingSystem(_collidersRegistryService))
                .AddSystem(new MakeAttackSystem())
                .AddSystem(new DamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        private Entity CreateEntity() => new Entity();
    }
}