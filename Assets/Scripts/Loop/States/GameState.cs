using System;
using System.Linq;
using Health;
using Internal.Runtime.Dependencies.Core;
using Internal.Runtime.Flow.States;
using Internal.Runtime.Utilities;
using States.Injectors;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Winning;
using Object = UnityEngine.Object;

namespace Loop.States
{
    public class GameState : AState
    {
        private DependencyRecipe<DependencyList<AWinCondition>> _winConditions = DependencyInjector.GetRecipe<DependencyList<AWinCondition>>();
        private DependencyRecipe<CamerasInjector> _cameras = DependencyInjector.GetRecipe<CamerasInjector>();
        private DependencyRecipe<Player> _initialPlayer = DependencyInjector.GetRecipe<Player>();
        
        private VolumeProfile _volumeProfile;
        private Player _playerPrefab;
        private PlayerInput _input;
        private Player _player;
        
        private Quaternion _initialPlayerRotation;
        private Vector3 _initialPlayerPosition;
        private ChromaticAberration _chromaticAberration;
        private MotionBlur _motionBlur;
        private Vignette _vignette;
        
        public GameState(PlayerInput input, Player playerPrefab, VolumeProfile volumeProfile)
        {
            _volumeProfile = volumeProfile;
            _playerPrefab = playerPrefab;
            _input = input;
        }

        public override void OnEnter()
        {
            Override(GetInitialPlayer());
            CacheRespawnData();
            StartListeningForRespawn();
            AssignCamera();
            GetVolumeOverrides();
            ToggleToGameCamera();
            EnableInput();
        }

        public override void OnExit()
        {
            StopListeningForRespawn();
            ResetVolumeOverrides();
        }

        public override Type OnUpdate() => HasWon() ? typeof(WonState) : null;

        private void RespawnPlayer()
        {
            TimeHelper.SetDefaultTimeScale();
            ResetVolumeOverrides();
            Init(GetNewPlayer());
            AssignCamera();
        }

        private Player GetNewPlayer() => Object.Instantiate(_playerPrefab, _initialPlayerPosition, _initialPlayerRotation);

        private void Init(Player player)
        {
            StopListeningForRespawn();
            Override(player);
            StartListeningForRespawn();
        }

        private Player GetInitialPlayer() => _initialPlayer.Value;

        private bool HasWon() => _winConditions.Value.All(winCondition => winCondition.IsFulfilled);

        private void EnableInput() => _input.actions.Enable();

        private void CacheRespawnData()
        {
            _initialPlayerPosition = _initialPlayer.Value.Position;
            _initialPlayerRotation = _initialPlayer.Value.Rotation;
        }

        private void ToggleToGameCamera()
        {
            _cameras.Value.Game.Priority = 1;
            _cameras.Value.Menu.Priority = 0;
        }

        private void AssignCamera() => _cameras.Value.Game.Follow = _initialPlayer.Value.Center;

        private void Override(Player player) => _player = player;

        private void StartListeningForRespawn() => _player.OnDied += RespawnPlayer;

        private void StopListeningForRespawn() => _player.OnDied -= RespawnPlayer;

        private void GetVolumeOverrides()
        {
            _volumeProfile.TryGet(out _chromaticAberration);
            _volumeProfile.TryGet(out _motionBlur);
            _volumeProfile.TryGet(out _vignette);
        }

        private void ResetVolumeOverrides()
        {
            _chromaticAberration.intensity.value = 0f;
            _motionBlur.intensity.value = 0f;
            _vignette.intensity.value = 0f;
        }
    }
}