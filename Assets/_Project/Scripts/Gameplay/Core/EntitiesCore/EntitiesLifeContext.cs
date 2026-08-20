using System;
using System.Collections.Generic;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore
{
    public class EntitiesLifeContext : IDisposable
    {
        public event Action<Entity> Added;
        public event Action<Entity> Released;

        private readonly List<Entity> _entities = new();
        private readonly List<Entity> _releasedEntities = new();

        public void Add(Entity entity)
        {
            _entities.Add(entity);

            entity.Initialize();

            Added?.Invoke(entity);
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < _entities.Count; i++)
                _entities[i].OnUpdate(deltaTime);

            foreach (Entity entity in _releasedEntities)
            {
                _entities.Remove(entity);
                entity.Dispose();
                Released?.Invoke(entity);
            }

            _releasedEntities.Clear();
        }

        public void Release(Entity entity)
        {
            _releasedEntities.Add(entity);
        }

        public void Dispose()
        {
            foreach (Entity entity in _entities)
                entity.Dispose();

            _entities.Clear();
            _releasedEntities.Clear();
        }
    }
}