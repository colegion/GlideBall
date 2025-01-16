using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Scriptables;
using UnityEngine;
using UnityEngine.Serialization;

public class CustomPhysics : MonoBehaviour
{
    [SerializeField] private CustomPhysicsProperties physicsProperties;
    [SerializeField] private Utilities.Constraints bodyConstraints;
    [SerializeField] private GameObject rocketman;
    
    public float mass;
    public Vector3 acceleration;
    public Vector3 velocity;
    private bool _movementStarted;
    private bool _wingsOpen;
    
    public void AddForce(Vector3 force)
    {
        _movementStarted = true;
        acceleration = force / mass;
    }

    public void ReactionOnCollision()
    {
        acceleration.y = 0f;
    }

    private Vector3 _amountToLoseOnFriction;
    private void FixedUpdate()
    {
        if (!_movementStarted) return;
        
        var freezePosX = bodyConstraints.freezePositionX ? 0 : 1;
        var freezePosY = bodyConstraints.freezePositionY ? 0 : 1;
        var freezePosZ = bodyConstraints.freezePositionZ ? 0 : 1;
        acceleration.y -= physicsProperties.gravity;
        velocity += acceleration * Time.fixedDeltaTime;
        var displacement = velocity * Time.fixedDeltaTime;
        
        transform.Translate(displacement, Space.World);
        
        
        if (_wingsOpen)
        {
            rocketman.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            rocketman.transform.Rotate(new Vector3(20, 0, 0));
        }

        acceleration = Vector3.zero;
    }

    private void ApplyFriction(ref float value)
    {
        if (Mathf.Approximately(value, 0))
        {
            value = 0; 
            return;
        }

        int direction = GetDirection(value);
        value -= physicsProperties.friction * direction;
        
        if (Mathf.Abs(value) < physicsProperties.friction)
        {
            value = 0;
        }
    }

    private int GetDirection(float value)
    {
        return (int)(value / Mathf.Abs(value));
    }

    public void SetWingsStatus(bool value)
    {
        _wingsOpen = value;
    }
}
