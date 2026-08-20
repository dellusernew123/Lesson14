using _Project.Scripts.Gameplay.Core.ReactiveVariable;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Core.EntitiesCore.MovementFeature
{
    public class Energy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class MaxEnergy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class MinEnergyForTeleportation : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class EnergyPriceForTeleportation : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class EnergyRegenerationTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class TeleportationRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class GameObjectComponent : IEntityComponent
    {
        public GameObject Value;
    }

    public class MakeTeleportationRequest : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class MakeTeleportationEvent : IEntityComponent
    {
        public ReactiveEvent<Vector3> Value;
    }
}