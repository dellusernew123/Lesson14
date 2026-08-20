using System;
using _Project.Scripts.Gameplay.Core.Conditions;
using _Project.Scripts.Gameplay.Core.ReactiveVariable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore.MovementFeature.RigidbodySystems.Mover
{
    public class TeleportationMovementSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _energy;
        private ReactiveVariable<float> _maxEnergy;
        private ReactiveVariable<float> _minEnergyForTeleportation;
        private ReactiveVariable<float> _energyPriceForTeleportation;

        private ReactiveVariable<float> _teleportationRadius;

        private ReactiveEvent _teleportationRequest;
        private ReactiveEvent<Vector3> _teleportationEvent;
        private ICompositeCondition _canMakeTeleportation;

        private IDisposable _requestDisposable;

        private GameObject _gameObject;

        private ReactiveVariable<float> _energyRegenerationTime;
        private float _currentTime;

        public void OnInit(Entity entity)
        {
            _energy = entity.Energy;
            _maxEnergy = entity.MaxEnergy;
            _energyPriceForTeleportation = entity.EnergyPriceForTeleportation;
            _minEnergyForTeleportation = entity.MinEnergyForTeleportation;
            _gameObject = entity.GameObjectComponent;

            _teleportationRequest = entity.MakeTeleportationRequest;
            _teleportationEvent = entity.MakeTeleportationEvent;


            _teleportationRadius = entity.TeleportationRadius;

            _energyRegenerationTime = entity.EnergyRegenerationTime;
            _currentTime = _energyRegenerationTime.Value;

            _requestDisposable = _teleportationRequest.Subscribe(OnTeleportationRequest);
        }

        private void OnTeleportationRequest()
        {
            if (_energy.Value < _minEnergyForTeleportation.Value)
                return;

            _energy.Value = Mathf.Max(_energy.Value - _energyPriceForTeleportation.Value, 0);

            Vector3 teleportationPosition = new Vector3
            (
                RandomTeleportationPosition(),
                0,
                RandomTeleportationPosition()
            );

            _gameObject.transform.position = teleportationPosition;
            _teleportationEvent.Invoke(teleportationPosition);
        }

        public void OnUpdate(float deltaTime)
        {
            _currentTime -= deltaTime;

            if (_currentTime <= 0)
            {
                _energy.Value = Mathf.Min(_energy.Value + _maxEnergy.Value * 0.1f, _maxEnergy.Value);
                Debug.Log(_energy.Value);
                _currentTime = _energyRegenerationTime.Value;
            }
        }

        public void OnDispose()
        {
            _requestDisposable.Dispose();
        }

        private float RandomTeleportationPosition()
            => Random.Range(-_teleportationRadius.Value, _teleportationRadius.Value);
    }
}