using _Project.Scripts.Gameplay.Core.EntitiesCore.Buffer;
using _Project.Scripts.Gameplay.Core.ReactiveVariable;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore.Features.Sensors
{
    public class BodyCollider : IEntityComponent
    {
        public BoxCollider Value;
    }

    public class ContactsDetectingMask : IEntityComponent
    {
        public LayerMask Value;
    }

    public class ContactColliderBuffer : IEntityComponent
    {
        public Buffer<Collider> Value;
    }

    public class ContactEntitiesBuffer : IEntityComponent
    {
        public Buffer<Entity> Value;
    }

    public class FindCollidersEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class ContactsDetectedEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
}