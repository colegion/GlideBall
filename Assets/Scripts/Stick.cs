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
    private float _lastTilt;
    
    public void TiltBones(float angle)
    {
        _lastTilt = (angle / Screen.width) * _sensitivity;
        if (_lastTilt < 0) _lastTilt = 0;
        foreach (var bone in bones)
        {
            bone.transform.localRotation = Quaternion.Euler(new Vector3(-_lastTilt, 0, 0));
        }
    }

    public float GetLastTilt()
    {
        return _lastTilt;
    }
}
