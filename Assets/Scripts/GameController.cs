using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Stick stick;
    [SerializeField] private Player player;
    private InputController _inputController;

    private static GameController _instance;

    public static GameController Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        _inputController = new InputController();
        _inputController.GeneralInputs.Tilt.Enable();
        _inputController.GeneralInputs.OnTap.Enable();
    }
    
    private void Update()
    {
        if (_inputController == null) return;
        if (_inputController.GeneralInputs.OnTap.IsPressed())
        {
            var tilt = _inputController.GeneralInputs.Tilt.ReadValue<Vector2>();
            Debug.Log($"tilt : {tilt}");
            stick.TiltBones(tilt.x);
        }
        
        if(_inputController.GeneralInputs.OnTap.WasReleasedThisFrame())
        {
            var tilt = stick.GetLastTilt();
            if (tilt > 0)
            {
                _inputController.GeneralInputs.TriggerGlide.Enable();
                _inputController.GeneralInputs.Tilt.Disable();
                stick.PlayReleaseAnimation();
                player.HandleOnStickReleased(tilt);
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
        }

        if (_inputController.GeneralInputs.Glide.enabled)
        {
            var delta = _inputController.GeneralInputs.Glide.ReadValue<Vector2>();
            Debug.Log($"swipe delta is : {delta}");
        }
    }
}
