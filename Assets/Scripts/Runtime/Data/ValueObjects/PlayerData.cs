using System;
using Unity.Mathematics;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public struct PlayerData
    {
        public PlayerMovementData MovementData;
    }

    [Serializable]
    public struct PlayerMovementData
    {
        public float ForwardSpeed;
        public float SprintSpeed;
    }
}