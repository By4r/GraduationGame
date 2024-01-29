using System;
using Unity.Mathematics;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public struct PlayerData
    {
        public PlayerMovementData MovementData;
        public PlayerFOVData PlayerFOVData;
    }

    [Serializable]
    public struct PlayerMovementData
    {
        public float ForwardSpeed;
        public float SprintSpeed;
    }
    [Serializable]
    public struct PlayerFOVData
    {
        public float NormalFOV;
        public float SprintFOV;
    }
    
}