using System;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Buffer;
using _Project.Scripts.Gameplay.Core.ReactiveVariable;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore.Features.Sensors
{
    public class BodyContactsDetectingSystem : IInitializableSystem, IDisposableSystem
    {
        private readonly CollidersRegistryService _collidersRegistryService;

        private Buffer<Collider> _contacts;
        private Buffer<Entity> _contactsEntities;

        private LayerMask _mask;
        private BoxCollider _body;

        private ReactiveVariable<float> _attackRadius;

        private ReactiveEvent<Vector3> _teleportationEvent;
        private ReactiveEvent _contactsDetectedEvent;

        private IDisposable _teleportationDisposable;

        private Entity _entity;

        public BodyContactsDetectingSystem(CollidersRegistryService collidersRegistryService)
        {
            _collidersRegistryService = collidersRegistryService;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;

            _contacts = entity.ContactColliderBuffer;
            _contactsEntities = entity.ContactEntitiesBuffer;

            _mask = entity.ContactsDetectingMask;
            _body = entity.BodyCollider;
            _attackRadius = entity.AttackRadius;

            _teleportationEvent = entity.MakeTeleportationEvent;
            _contactsDetectedEvent = entity.ContactsDetectedEvent;

            _teleportationDisposable = _teleportationEvent.Subscribe(OnTeleportation);
        }

        private void OnTeleportation(Vector3 position)
        {
            DetectColliders();
            ConvertCollidersToEntities();

            _contactsDetectedEvent.Invoke();
        }

        private void DetectColliders()
        {
            _contacts.Count = Physics.OverlapSphereNonAlloc
            (
                _body.bounds.center,
                _attackRadius.Value,
                _contacts.Items,
                _mask,
                QueryTriggerInteraction.Ignore
            );

            RemoveSelfFromContacts();
        }

        private void ConvertCollidersToEntities()
        {
            _contactsEntities.Count = 0;

            for (int i = 0; i < _contacts.Count; i++)
            {
                Collider collider = _contacts.Items[i];

                Entity contactEntity = _collidersRegistryService.GetBy(collider);

                _contactsEntities.Items[_contactsEntities.Count] = contactEntity;

                _contactsEntities.Count++;
            }
        }

        private void RemoveSelfFromContacts()
        {
            for (int i = 0; i < _contacts.Count; i++)
            {
                if (_contacts.Items[i] != _body)
                    continue;

                for (int j = i; j < _contacts.Count - 1; j++)
                    _contacts.Items[j] = _contacts.Items[j + 1];

                _contacts.Count--;
                return;
            }
        }

        public void OnDispose()
        {
            _teleportationDisposable?.Dispose();
        }
    }
}