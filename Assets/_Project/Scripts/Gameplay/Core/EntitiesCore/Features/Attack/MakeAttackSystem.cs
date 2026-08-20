using System;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Buffer;
using _Project.Scripts.Gameplay.Core.ReactiveVariable;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore.Features.Attack
{
    public class MakeAttackSystem : IInitializableSystem, IDisposableSystem
    {
        private Buffer<Entity> _contacts;
        private ReactiveVariable<float> _damage;

        private ReactiveEvent _contactsDetectedEvent;
        private IDisposable _contactsDisposable;

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactEntitiesBuffer;
            _damage = entity.AttackDamage;

            _contactsDetectedEvent = entity.ContactsDetectedEvent;

            _contactsDisposable = _contactsDetectedEvent.Subscribe(OnContactsDetected);
        }

        private void OnContactsDetected()
        {
            for (int i = 0; i < _contacts.Count; i++)
            {
                Attack(_contacts.Items[i]);
            }
        }

        public void Attack(Entity entity)
        {
            entity.TakeDamageRequest.Invoke(_damage.Value);
        }

        public void OnDispose()
        {
            _contactsDisposable?.Dispose();
        }
    }
}