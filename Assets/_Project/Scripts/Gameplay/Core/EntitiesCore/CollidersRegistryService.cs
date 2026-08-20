using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore
{
    public class CollidersRegistryService
    {
        private readonly Dictionary<Collider, Entity> _collidersToEntity = new();

        public void Register(Collider collider, Entity entity)
        {
            _collidersToEntity.Add(collider, entity);
        }

        public void Unregister(Collider collider)
        {
            _collidersToEntity.Remove(collider);
        }

        public Entity GetBy(Collider collider)
        {
            if (_collidersToEntity.TryGetValue(collider, out Entity entity))
                return entity;

            return null;
        }
    }
}