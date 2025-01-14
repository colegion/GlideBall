using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
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

    public InputController GetInputController()
    {
        return _inputController;
    }
}
