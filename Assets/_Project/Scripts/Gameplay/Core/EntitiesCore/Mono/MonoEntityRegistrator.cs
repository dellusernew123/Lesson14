using UnityEngine;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore.Mono
{
    public abstract class MonoEntityRegistrator : MonoBehaviour
    {
        public abstract void Register(Entity entity);
    }
}