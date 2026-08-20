using _Project.Scripts.Gameplay.Core.Conditions;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Buffer;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Features.ApplyDamage;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Features.Attack;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Features.LifeCycle;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Features.Sensors;
using _Project.Scripts.Gameplay.Core.EntitiesCore.MovementFeature;
using _Project.Scripts.Gameplay.Core.ReactiveVariable;
using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore
{
    public partial class Entity
    {

        public AttackDamage AttackDamageC => GetComponent<AttackDamage>();
        public ReactiveVariable<Single> AttackDamage => AttackDamageC.Value;

        public Entity AddAttackDamage(ReactiveVariable<Single> value)
            => AddComponent(new AttackDamage() { Value = value });

        public AttackRadius AttackRadiusC => GetComponent<AttackRadius>();
        public ReactiveVariable<Single> AttackRadius => AttackRadiusC.Value;

        public Entity AddAttackRadius(ReactiveVariable<Single> value)
            => AddComponent(new AttackRadius() { Value = value });

        public BodyCollider BodyColliderC => GetComponent<BodyCollider>();
        public BoxCollider BodyCollider => BodyColliderC.Value;

        public Entity AddBodyCollider(BoxCollider value)
            => AddComponent(new BodyCollider() { Value = value });

        public CanApplyDamage CanApplyDamageC => GetComponent<CanApplyDamage>();
        public ICompositeCondition CanApplyDamage => CanApplyDamageC.Value;

        public Entity AddCanApplyDamage(ICompositeCondition value)
            => AddComponent(new CanApplyDamage() { Value = value });

        public ContactColliderBuffer ContactColliderBufferC => GetComponent<ContactColliderBuffer>();
        public Buffer<Collider> ContactColliderBuffer => ContactColliderBufferC.Value;

        public Entity AddContactColliderBuffer(Buffer<Collider> value)
            => AddComponent(new ContactColliderBuffer() { Value = value });

        public ContactEntitiesBuffer ContactEntitiesBufferC => GetComponent<ContactEntitiesBuffer>();
        public Buffer<Entity> ContactEntitiesBuffer => ContactEntitiesBufferC.Value;

        public Entity AddContactEntitiesBuffer(Buffer<Entity> value)
            => AddComponent(new ContactEntitiesBuffer() { Value = value });

        public ContactsDetectedEvent ContactsDetectedEventC => GetComponent<ContactsDetectedEvent>();
        public ReactiveEvent ContactsDetectedEvent => ContactsDetectedEventC.Value;

        public Entity AddContactsDetectedEvent(ReactiveEvent value)
            => AddComponent(new ContactsDetectedEvent() { Value = value });

        public ContactsDetectingMask ContactsDetectingMaskC => GetComponent<ContactsDetectingMask>();
        public LayerMask ContactsDetectingMask => ContactsDetectingMaskC.Value;

        public Entity AddContactsDetectingMask(LayerMask value)
            => AddComponent(new ContactsDetectingMask() { Value = value });

        public CurrentHealth CurrentHealthC => GetComponent<CurrentHealth>();
        public ReactiveVariable<Single> CurrentHealth => CurrentHealthC.Value;

        public Entity AddCurrentHealth(ReactiveVariable<Single> value)
            => AddComponent(new CurrentHealth() { Value = value });

        public Energy EnergyC => GetComponent<Energy>();
        public ReactiveVariable<Single> Energy => EnergyC.Value;

        public Entity AddEnergy(ReactiveVariable<Single> value)
            => AddComponent(new Energy() { Value = value });

        public EnergyPriceForTeleportation EnergyPriceForTeleportationC => GetComponent<EnergyPriceForTeleportation>();
        public ReactiveVariable<Single> EnergyPriceForTeleportation => EnergyPriceForTeleportationC.Value;

        public Entity AddEnergyPriceForTeleportation(ReactiveVariable<Single> value)
            => AddComponent(new EnergyPriceForTeleportation() { Value = value });

        public EnergyRegenerationTime EnergyRegenerationTimeC => GetComponent<EnergyRegenerationTime>();
        public ReactiveVariable<Single> EnergyRegenerationTime => EnergyRegenerationTimeC.Value;

        public Entity AddEnergyRegenerationTime(ReactiveVariable<Single> value)
            => AddComponent(new EnergyRegenerationTime() { Value = value });

        public FindCollidersEvent FindCollidersEventC => GetComponent<FindCollidersEvent>();
        public ReactiveEvent FindCollidersEvent => FindCollidersEventC.Value;

        public Entity AddFindCollidersEvent(ReactiveEvent value)
            => AddComponent(new FindCollidersEvent() { Value = value });

        public GameObjectComponent GameObjectComponentC => GetComponent<GameObjectComponent>();
        public GameObject GameObjectComponent => GameObjectComponentC.Value;

        public Entity AddGameObjectComponent(GameObject value)
            => AddComponent(new GameObjectComponent() { Value = value });

        public IsDead IsDeadC => GetComponent<IsDead>();
        public ReactiveVariable<Boolean> IsDead => IsDeadC.Value;

        public Entity AddIsDead(ReactiveVariable<Boolean> value)
            => AddComponent(new IsDead() { Value = value });

        public MakeTeleportationEvent MakeTeleportationEventC => GetComponent<MakeTeleportationEvent>();
        public ReactiveEvent<Vector3> MakeTeleportationEvent => MakeTeleportationEventC.Value;

        public Entity AddMakeTeleportationEvent(ReactiveEvent<Vector3> value)
            => AddComponent(new MakeTeleportationEvent() { Value = value });

        public MakeTeleportationRequest MakeTeleportationRequestC => GetComponent<MakeTeleportationRequest>();
        public ReactiveEvent MakeTeleportationRequest => MakeTeleportationRequestC.Value;

        public Entity AddMakeTeleportationRequest(ReactiveEvent value)
            => AddComponent(new MakeTeleportationRequest() { Value = value });

        public MaxEnergy MaxEnergyC => GetComponent<MaxEnergy>();
        public ReactiveVariable<Single> MaxEnergy => MaxEnergyC.Value;

        public Entity AddMaxEnergy(ReactiveVariable<Single> value)
            => AddComponent(new MaxEnergy() { Value = value });

        public MaxHealth MaxHealthC => GetComponent<MaxHealth>();
        public ReactiveVariable<Single> MaxHealth => MaxHealthC.Value;

        public Entity AddMaxHealth(ReactiveVariable<Single> value)
            => AddComponent(new MaxHealth() { Value = value });

        public MinEnergyForTeleportation MinEnergyForTeleportationC => GetComponent<MinEnergyForTeleportation>();
        public ReactiveVariable<Single> MinEnergyForTeleportation => MinEnergyForTeleportationC.Value;

        public Entity AddMinEnergyForTeleportation(ReactiveVariable<Single> value)
            => AddComponent(new MinEnergyForTeleportation() { Value = value });

        public TakeDamageEvent TakeDamageEventC => GetComponent<TakeDamageEvent>();
        public ReactiveEvent<Single> TakeDamageEvent => TakeDamageEventC.Value;

        public Entity AddTakeDamageEvent(ReactiveEvent<Single> value)
            => AddComponent(new TakeDamageEvent() { Value = value });

        public TakeDamageRequest TakeDamageRequestC => GetComponent<TakeDamageRequest>();
        public ReactiveEvent<Single> TakeDamageRequest => TakeDamageRequestC.Value;

        public Entity AddTakeDamageRequest(ReactiveEvent<Single> value)
            => AddComponent(new TakeDamageRequest() { Value = value });

        public TeleportationRadius TeleportationRadiusC => GetComponent<TeleportationRadius>();
        public ReactiveVariable<Single> TeleportationRadius => TeleportationRadiusC.Value;

        public Entity AddTeleportationRadius(ReactiveVariable<Single> value)
            => AddComponent(new TeleportationRadius() { Value = value });
    }
}
