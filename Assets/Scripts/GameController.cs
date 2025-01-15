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
            Debug.Log("tilt :" + tilt);
            stick.TiltBones(tilt.x);
        }
        
        if(_inputController.GeneralInputs.OnTap.WasReleasedThisFrame())
        {
            //stick.TiltBones(0);
            _inputController.GeneralInputs.BeginGlide.Enable();
            stick.PlayReleaseAnimation();
            player.HandleOnStickReleased(stick.GetLastTilt());
        }

        if (_inputController.GeneralInputs.BeginGlide.IsPressed())
        {
            player.ToggleWings(true);
        }
        
        if (_inputController.GeneralInputs.BeginGlide.WasReleasedThisFrame())
        {
            player.ToggleWings(false);
        }
    }
}
