using System;
using UnityEngine;

namespace Movement.Data
{
    [Serializable]
    public class Settings
    {
        [field: Header("Movement")]
        [field: SerializeField] public float AccelerationRate { get; set; }
        [field: SerializeField] public float DecelerationRate { get; set; }
        [field: SerializeField] public float MoveSpeed { get; set; }
    
        [field: Header("Jumping")]
        [field: SerializeField] public float FinishJumpMultiplayer { get; set; }
        [field: SerializeField] public float CoyoteDuration { get; set; }
        [field: SerializeField] public float BufferDuration { get; set; }
        [field: SerializeField] public float JumpForce { get; set; }
    
        [field: Header("Dashing")]
        [field: SerializeField] public float DashCooldown { get; set; }
        [field: SerializeField] public float DashDuration { get; set; }
        [field: SerializeField] public float DashForce { get; set; }
    }
}