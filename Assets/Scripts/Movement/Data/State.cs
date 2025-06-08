using System;
using UnityEngine;

namespace Movement.Data
{
    public interface IDasherState : IDashState, IReadOnlyGroundedState, IReadOnlyDashInput
    {
    }

    public interface IMoverState : IReadOnlyDashState, IReadOnlyMoveInput
    {
    }

    public interface IJumperState : IReadOnlyGroundedState, IReadOnlyDashState, IReadOnlyJumpInput
    {
    }
    
    public interface IDashState
    {
        bool IsDashing { get; set; }
    }
    
    public interface IReadOnlyDashState
    {
        bool IsDashing { get; }
    }

    public interface IReadOnlyGroundedState
    {
        bool IsGrounded { get; }
    }

    public interface IReadOnlyJumpInput
    {
        bool GotFinishJumpInput { get; }
        bool GotJumpInput { get; }
    }

    public interface IReadOnlyDashInput
    {
        bool GotDashInput { get; }
    }

    public interface IReadOnlyMoveInput
    {
        float HorizontalInput { get; }
    }

    [Serializable]
    public class State : IDasherState, IMoverState, IJumperState
    {
        [field: Header("Input")]
        [field: SerializeField] public float HorizontalInput { get; set; }
        [field: SerializeField] public bool GotFinishJumpInput { get; set; }
        [field: SerializeField] public bool GotJumpInput { get; set; }
        [field: SerializeField] public bool GotDashInput { get; set; }
        
        [field: Header("Other")]
        [field: SerializeField] public bool IsGrounded { get; set; }
        [field: SerializeField] public bool IsDashing { get; set; }
    }
}