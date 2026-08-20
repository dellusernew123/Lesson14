namespace _Project.Scripts.Gameplay.Core.EntitiesCore
{
    public interface IUpdatableSystem : IEntitySystem
    {
        void OnUpdate(float deltaTime);
    }
}