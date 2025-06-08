using Internal.Runtime.Utilities;
using Movement.Data;
using UnityEngine;

namespace Movement.Composites
{
    public class Mover : AComposite
    {
        private float _moveSpeed;
        private float _accelerationRate;
        private float _decelerationRate;
        private Rigidbody2D _playerRigidbody;
        private Transform _playerTransform;
        private Transform _healthTransform;
        private IMoverState _moverState;
        
        private float _speedDifference;
        private float _targetSpeed;
        
        private static readonly Vector3 RightLookScale = new Vector3(1f, 1f, 1f);
        private static readonly Vector3 LeftLookScale = new Vector3(-1f, 1f, 1f);

        private const float MoveSpeedThreshold = 0.05f;
        private const float SpeedDifferencePow = 0.9f;
        
        #region Constructorlike Methods

        protected override void Read(References references)
        {
            _playerRigidbody = references.PlayerRigidbody;
            _playerTransform = references.PlayerTransform;
            _healthTransform = references.HealthTransform;
        }

        protected override void Read(Settings settings)
        {
            _accelerationRate = settings.AccelerationRate;
            _decelerationRate = settings.DecelerationRate;
            _moveSpeed = settings.MoveSpeed;
        }

        protected override void Read(State state) => _moverState = state;
        
        #endregion

        public void HandleFacingDirection()
        {
            if (!ShouldRotateHorizontally())
            {
                return;
            }
            
            RotateHorizontally();
        }

        public void HandleHorizontalMovement()
        {
            if (!ShouldMoveHorizontally())
            {
                return;
            }
            
            CalculateHorizontalMovement();
            ApplyHorizontalMovement();
        }

        private bool ShouldRotateHorizontally()
        {
            if (_moverState.HorizontalInput.AbsoluteValue() < MoveSpeedThreshold)
            {
                return false;
            }

            return !_moverState.IsDashing;
        }

        private void RotateHorizontally() =>  _playerTransform.localScale = _healthTransform.localScale = _moverState.HorizontalInput > 0f ? RightLookScale : LeftLookScale;

        private bool ShouldMoveHorizontally() => !_moverState.IsDashing;
        
        private void CalculateHorizontalMovement()
        {
            _targetSpeed = _moverState.HorizontalInput * _moveSpeed;
            _speedDifference = _targetSpeed - _playerRigidbody.velocity.x;
        }

        private void ApplyHorizontalMovement()
        {
            float accelerationFactor = Mathf.Abs(_targetSpeed) > MoveSpeedThreshold ? _accelerationRate : _decelerationRate;
            float lackingForce = Mathf.Pow(Mathf.Abs(_speedDifference) * accelerationFactor, SpeedDifferencePow);
            float moveDirection = Mathf.Sign(_speedDifference);

            _playerRigidbody.AddForce(lackingForce * moveDirection * Vector2.right);
        }
    }
}