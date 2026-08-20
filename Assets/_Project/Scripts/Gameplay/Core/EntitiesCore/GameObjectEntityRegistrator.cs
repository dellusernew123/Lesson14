using _Project.Scripts.Gameplay.Core.EntitiesCore.Mono;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore
{
    public class GameObjectEntityRegistrator : MonoEntityRegistrator
    {
        public override void Register(Entity entity)
        {
            entity.AddGameObjectComponent(gameObject);
        }
    }
}