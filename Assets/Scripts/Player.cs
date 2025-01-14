using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CustomPhysics physics;
    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void HandleOnStickReleased(float tilt)
    {
        physics.AddForce(new Vector3(tilt, 0, 0));
    }
    
    private void AddListeners()
    {
        Stick.OnStickReleased += HandleOnStickReleased;
    }

    private void RemoveListeners()
    {
        Stick.OnStickReleased -= HandleOnStickReleased;
    }
}
