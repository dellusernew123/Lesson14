using _Project.Scripts.Gameplay.Core.EntitiesCore.Mono;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore
{
    [RequireComponent(typeof(BoxCollider))]
    public class BodyColliderEntityRegistrator : MonoEntityRegistrator
    {
        public override void Register(Entity entity)
        {
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            entity.AddBodyCollider(boxCollider);
        }
    }
}