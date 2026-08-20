namespace _Project.Scripts.Gameplay.Core.EntitiesCore
{
    public interface IInitializableSystem : IEntitySystem
    {
        void OnInit(Entity entity);
    }
}