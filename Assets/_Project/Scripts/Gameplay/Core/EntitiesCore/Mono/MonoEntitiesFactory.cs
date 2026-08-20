using System;
using System.Collections.Generic;
using _Project.Scripts.Combined.CoreModules.AssetsManagement;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore.Mono
{
    public class MonoEntitiesFactory : IInitializable, IDisposable
    {
        private readonly ResourcesAssetsLoader _loader;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        private readonly CollidersRegistryService _collidersRegistryService;

        private readonly Dictionary<Entity, MonoEntity> _entityToMono = new();

        public MonoEntitiesFactory
        (
            ResourcesAssetsLoader loader,
            EntitiesLifeContext entitiesLifeContext,
            CollidersRegistryService collidersRegistryService
        )
        {
            _loader = loader;
            _entitiesLifeContext = entitiesLifeContext;
            _collidersRegistryService = collidersRegistryService;
        }

        public MonoEntity Create(Entity entity, Vector3 position, string path)
        {
            MonoEntity prefab = _loader.Load<MonoEntity>(path);
            MonoEntity viewInstance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            viewInstance.Initialize(_collidersRegistryService);

            viewInstance.Link(entity);

            _entityToMono.Add(entity, viewInstance);

            return viewInstance;
        }

        public void Initialize()
        {
            _entitiesLifeContext.Released += OnEntityReleased;
        }

        public void Dispose()
        {
            _entitiesLifeContext.Released -= OnEntityReleased;

            foreach (Entity entity in _entityToMono.Keys)
                CleanupFor(entity);

            _entityToMono.Clear();
        }

        private void OnEntityReleased(Entity entity)
        {
            CleanupFor(entity);

            _entityToMono.Remove(entity);
        }

        private void CleanupFor(Entity entity)
        {
            MonoEntity monoEntity = _entityToMono[entity];
            monoEntity.Cleanup(entity);
            Object.Destroy(monoEntity.gameObject);
        }
    }
}