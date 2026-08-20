using _Project.Scripts.Gameplay.Core.ReactiveVariable;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore.Features.LifeCycle
{
    public class DeathSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<float> _currentHealth;

        public void OnInit(Entity entity)
        {
            _isDead = entity.IsDead;
            _currentHealth = entity.CurrentHealth;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value == true)
                return;

            if (_currentHealth.Value <= 0)
            {
                _isDead.Value = true;
                Debug.Log("dead");
            }
        }
    }
}