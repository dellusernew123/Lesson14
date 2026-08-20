using System;
using _Project.Scripts.Gameplay.Core.Conditions;
using _Project.Scripts.Gameplay.Core.ReactiveVariable;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore.Features.ApplyDamage
{
    public class DamageSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent<float> _damageRequest;
        private ReactiveEvent<float> _damageEvent;

        private ReactiveVariable<float> _health;
        private ICompositeCondition _canApplyDamage;

        private IDisposable _requestDisposable;

        public void OnInit(Entity entity)
        {
            _damageRequest = entity.TakeDamageRequest;
            _damageEvent = entity.TakeDamageEvent;

            _health = entity.CurrentHealth;

            _canApplyDamage = entity.CanApplyDamage;
            _requestDisposable = _damageRequest.Subscribe(OnDamageRequest);
        }

        public void OnDispose()
        {
            _requestDisposable.Dispose();
        }

        private void OnDamageRequest(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage), damage, "Value cannot be negative.");

            if (_canApplyDamage.Evaluate() == false)
                return;

            _health.Value = Mathf.Max(_health.Value - damage, 0);

            float appliedDamage = Mathf.Min(damage, _health.Value);

            _health.Value -= appliedDamage;

            _damageEvent.Invoke(appliedDamage);
            Debug.Log("ApplyDamageSystem OnDamageRequest: " + damage);
        }
    }
}