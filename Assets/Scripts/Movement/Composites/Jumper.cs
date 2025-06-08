using Movement.Data;
using UnityEngine;

namespace Movement.Composites
{
    public class Jumper : AComposite
    {
        private float _jumpForce;
        private float _finishJumpMultiplayer;
        private float _coyoteDuration;
        private float _bufferDuration;
        private Rigidbody2D _playerRigidbody;
        private IJumperState _jumperState;
        
        private float _bufferedJumpTime;
        private float _coyoteJumpTime;
        private bool _isJumping;
        
        #region Constructorlike Methods

        protected override void Read(References references) => _playerRigidbody = references.PlayerRigidbody;

        protected override void Read(Settings settings)
        {
            _finishJumpMultiplayer = settings.FinishJumpMultiplayer;
            _coyoteDuration = settings.CoyoteDuration;
            _bufferDuration = settings.BufferDuration;
            _jumpForce = settings.JumpForce;
        }

        protected override void Read(State state) => _jumperState = state;
        
        #endregion
        
        public void HandleJumping()
        {
            RefreshJumpTime();
            RefreshCoyoteTime();

            if (ShouldBeginJump())
            {
                InvalidateJumpInput();
                BeginJump();
            }

            if (ShouldFinishJump())
            {
                FinishJump();
            }
        }

        private void RefreshJumpTime()
        {
            if (!_jumperState.GotJumpInput)
            {
                return;
            }
            
            _bufferedJumpTime = _bufferDuration + Time.time;
        }

        private void RefreshCoyoteTime()
        {
            if (!_jumperState.IsGrounded)
            {
                return;
            }

            _coyoteJumpTime = _coyoteDuration + Time.time;
        }

        private bool ShouldBeginJump() => !_jumperState.IsDashing && _bufferedJumpTime > Time.time && _coyoteJumpTime > Time.time;

        private bool ShouldFinishJump() => _isJumping && _playerRigidbody.velocity.y > 0f && _jumperState.GotFinishJumpInput;

        private void InvalidateJumpInput() => _bufferedJumpTime = 0f;

        private void BeginJump()
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _jumpForce);
            _isJumping = true;
        }

        private void FinishJump()
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _playerRigidbody.velocity.y * _finishJumpMultiplayer);
            _isJumping = false;
        }
    }
}