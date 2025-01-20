using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Stick stick;
    [SerializeField] private Player player;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlatformController platformController;
    [SerializeField] private GroundHelper groundHelper;
    private InputController _inputController;

    private bool _acceptInput = true;

    private void OnEnable()
    {
        _inputController = new InputController();
        _inputController.GeneralInputs.Tilt.Enable();
        _inputController.GeneralInputs.OnTap.Enable();
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }
    
    private void Update()
    {
        if(!_acceptInput) return;
        if (_inputController == null) return;
        if (_inputController.GeneralInputs.OnTap.IsPressed())
        {
            var tilt = _inputController.GeneralInputs.Tilt.ReadValue<Vector2>();
            stick.TiltBones(tilt.x);
        }
        
        if(_inputController.GeneralInputs.OnTap.WasReleasedThisFrame())
        {
            var tilt = stick.GetLastTilt();
            if (tilt > 0)
            {
                _inputController.GeneralInputs.TriggerGlide.Enable();
                _inputController.GeneralInputs.Tilt.Disable();
                player.transform.SetParent(null);
                stick.PlayReleaseAnimation();
                cameraController.SetTarget(player.transform, () =>
                {
                    player.HandleOnStickReleased(tilt);
                });
                groundHelper.SetTarget(player);
                platformController.SetMovementStarted(true);
            }
        }

        if (_inputController.GeneralInputs.TriggerGlide.IsPressed())
        {
            player.ToggleWings(true);
            _inputController.GeneralInputs.Glide.Enable();
        }
        
        if (_inputController.GeneralInputs.TriggerGlide.WasReleasedThisFrame())
        {
            player.ToggleWings(false);
            _inputController.GeneralInputs.Glide.Disable();
            player.DisableGliding();
        }

        if (_inputController.GeneralInputs.Glide.enabled)
        {
            var delta = _inputController.GeneralInputs.Glide.ReadValue<Vector2>();
            player.TiltByAmount(delta);
        }
    }
    
    
    private void HandleOnBallGrounded()
    {
        _acceptInput = false;
    }

    private void RestartGame()
    {
        player.ResetSelf();
        cameraController.ResetSelf();
        stick.RePositionPlayer(player);
        platformController.ResetPool();
        ResetInputStates();
        _acceptInput = true;
    }

    private void ResetInputStates()
    {
        _inputController.GeneralInputs.TriggerGlide.Disable();
        _inputController.GeneralInputs.Tilt.Enable();
    }

    private void AddListeners()
    {
        CustomPhysics.OnBallGrounded += HandleOnBallGrounded;
        UIHelper.OnRestartRequested += RestartGame;
    }

    private void RemoveListeners()
    {
        CustomPhysics.OnBallGrounded -= HandleOnBallGrounded;
        UIHelper.OnRestartRequested -= RestartGame;
    }
}
