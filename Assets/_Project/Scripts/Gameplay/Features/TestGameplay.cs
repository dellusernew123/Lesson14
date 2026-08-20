using System;
using _Project.Scripts.Combined.CoreModules.AssetsManagement;
using _Project.Scripts.Gameplay.Core.EntitiesCore;
using _Project.Scripts.Gameplay.Core.EntitiesCore.Mono;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Features
{
    [Serializable]
    public class TestGameplay
    {
        public TestGameplay(Transform playerStartPosition, EntitiesLifeContext entitiesLifeContext)
        {
            _playerStartPosition = playerStartPosition;
            _entitiesLifeContext = entitiesLifeContext;
        }

        private EntitiesLifeContext _entitiesLifeContext;
        private EntitiesFactory _entitiesFactory;
        private Entity _entity;

        private Transform _playerStartPosition;

        private bool _isRunning;

        public void Initialize()
        {
            CollidersRegistryService collidersRegistryService = new CollidersRegistryService();
            ResourcesAssetsLoader resourcesLoader = new ResourcesAssetsLoader();
            MonoEntitiesFactory monoEntitiesFactory =
                new MonoEntitiesFactory(resourcesLoader, _entitiesLifeContext, collidersRegistryService);

            monoEntitiesFactory.Initialize();

            _entitiesFactory = new EntitiesFactory
            (
                _entitiesLifeContext,
                monoEntitiesFactory,
                collidersRegistryService
            );
        }

        public void Run()
        {
            _entity = _entitiesFactory.CreatePlayer(_playerStartPosition.position);

            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _entity.TakeDamageRequest.Invoke(50);
                Debug.Log("Has" + _entity.CurrentHealth.Value.ToString());
            }

            else if (Input.GetKeyDown(KeyCode.T))
            {
                _entity.MakeTeleportationRequest.Invoke();
            }
        }
    }
}