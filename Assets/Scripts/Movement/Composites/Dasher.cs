using DG.Tweening;
using Internal.Runtime.Utilities;
using Movement.Data;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Movement.Composites
{
    public class Dasher : AComposite
    {
        private VolumeProfile _volumeProfile;
        private GameObject[] _dashObjects;
        private Rigidbody2D _playerRigidbody;
        private Transform _playerTransform;
        private IDasherState _dasherState;
        private float _dashCooldown;
        private float _dashDuration;
        private float _dashForce;
        
        private ChromaticAberration _chromaticAberration;
        private MotionBlur _motionBlur;
        private Vignette _vignette;
        private float _dashFinishTime;
        private float _nextDashTime;
        private bool _wasDashing;
        
        private const float DashTimeScale = 0.5f;
        
        #region Constructorlike Methods
        
        protected override void Read(References references)
        {
            _playerRigidbody = references.PlayerRigidbody;
            _playerTransform = references.PlayerTransform;
            _volumeProfile = references.VolumeProfile;
            _dashObjects = references.DashObjects;
        }

        protected override void Read(Settings settings)
        {
            _dashDuration = settings.DashDuration;
            _dashCooldown = settings.DashCooldown;
            _dashForce = settings.DashForce;
        }

        protected override void Read(State state) => _dasherState = state;
        
        #endregion
        
        public void HandleDashing()
        {
            RefreshDashingState();
            
            if (ShouldBeginDash())
            {
                BeginDash();
            }

            if (ShouldEndDash())
            {
                EndDash();
            }
        }

        private void BeginDash()
        {
            ApplyHorizontalVelocity(DashHorizontalVelocity());
            MarkDashTiming();
            ToggleDashObjects(true);
            GetVolumeOverrides();
            ToggleVolumeOverrides(true);
            TimeHelper.SetTimeScale(DashTimeScale);
        }

        private void EndDash()
        {
            ApplyHorizontalVelocity(DefaultHorizontalVelocity());
            ToggleDashObjects(false);
            ToggleVolumeOverrides(false);
            TimeHelper.SetDefaultTimeScale();
        }

        private void ToggleVolumeOverrides(bool state)
        {
            DOTween.To(() => _chromaticAberration.intensity.value, value => _chromaticAberration.intensity.value = value, state ? 0.2f : 0f, 0.1f);
            DOTween.To(() => _motionBlur.intensity.value, value => _motionBlur.intensity.value = value, state ? 0.2f : 0f, 0.1f);
            DOTween.To(() => _vignette.intensity.value, value => _vignette.intensity.value = value, state ? 0.4f : 0f, 0.1f);
        }

        private void ApplyHorizontalVelocity(float horizontalVelocity) => _playerRigidbody.velocity = new Vector2(horizontalVelocity, 0f);

        private float DashHorizontalVelocity() => _dashForce * Mathf.Sign(_playerTransform.localScale.x);

        private float DefaultHorizontalVelocity() => _playerRigidbody.velocity.x;

        private void MarkDashTiming()
        {
            _dashFinishTime = _dashDuration + Time.time;
            _nextDashTime = _dashCooldown + Time.time;
        }

        private void GetVolumeOverrides()
        {
            _volumeProfile.TryGet(out _chromaticAberration);
            _volumeProfile.TryGet(out _motionBlur);
            _volumeProfile.TryGet(out _vignette);
        }

        private void ToggleDashObjects(bool state)
        {
            foreach (GameObject dashObject in _dashObjects)
            {
                dashObject.SetActive(state);
            }
        }

        private void RefreshDashingState()
        {
            bool isDashing = _dashFinishTime > Time.time;
            _wasDashing = _dasherState.IsDashing;
            _dasherState.IsDashing = isDashing;
        }

        private bool ShouldBeginDash()
        {
            if (_nextDashTime > Time.time)
            {
                return false;
            }
            
            if (!_dasherState.IsGrounded)
            {
                return false;
            }

            if (!_dasherState.GotDashInput)
            {
                return false;
            }

            return !_dasherState.IsDashing;
        }

        private bool ShouldEndDash()
        {
            if (!_wasDashing)
            {
                return false;
            }
            
            return !_dasherState.IsDashing;
        }
    }
}