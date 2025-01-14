using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Stick : MonoBehaviour
{
    [SerializeField] private Transform[] bones;

    private readonly float _sensitivity = 22f;

    private InputController _inputController;
    private float _lastTilt;

    public static event Action<float> OnStickReleased; 

    private void Start()
    {
        _inputController = GameController.Instance.GetInputController();
    }

    private void Update()
    {
        if (_inputController == null) return;
        if (_inputController.GeneralInputs.OnTap.IsPressed())
        {
            var tilt = _inputController.GeneralInputs.Tilt.ReadValue<Vector2>();
            Debug.Log("tilt :" + tilt);
            TiltBones(tilt.x);
        }
        
        if(_inputController.GeneralInputs.OnTap.WasReleasedThisFrame())
        {
            TiltBones(0);
            OnStickReleased?.Invoke(_lastTilt);
        }
    }

    private void TiltBones(float angle)
    {
        _lastTilt = (angle / Screen.width) * _sensitivity;
        if (_lastTilt < 0) _lastTilt = 0;
        foreach (var bone in bones)
        {
            bone.transform.localRotation = Quaternion.Euler(new Vector3(-_lastTilt, 0, 0));
        }
    }
}
