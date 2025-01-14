using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Stick : MonoBehaviour
{
    [SerializeField] private Transform[] bones;

    private float _maxTilt = 22f;

    private InputController _inputController;

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
    }

    public void TiltBones(float angle)
    {
        var tilt = (angle / Screen.width) * _maxTilt;
        if (tilt < 0) tilt = 0;
        foreach (var bone in bones)
        {
            bone.transform.localRotation = Quaternion.Euler(new Vector3(-tilt, 0, 0));
        }
    }
}
