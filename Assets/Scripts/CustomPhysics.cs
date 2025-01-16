using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Scriptables;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider))]
public class CustomPhysics : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    [SerializeField] private float bouncinessFactor;
    [SerializeField] private float rotationFactor;
    [SerializeField] private CustomPhysicsProperties physicsProperties;
    [SerializeField] private Utilities.Constraints bodyConstraints;
    [SerializeField] private GameObject rocketman;
    
    public float mass;
    public Vector3 acceleration;
    public Vector3 velocity;
    private bool _movementStarted;
    private bool _wingsOpen;

    private int _velocityFactor = 1;
    public void AddForce(Vector3 force)
    {
        _movementStarted = true;
        acceleration += force / mass;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Platform platform))
        {
            var boost = platform.GetBoostAmount();
            AddForce(new Vector3(0, boost, 0));
        }
        else
        {
            ReactionToCollisionEnter();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        ReactionToCollisionStay();
    }

    private void ReactionToCollisionEnter()
    {
        var reaction = new Vector3(velocity.x, -velocity.y, -velocity.z) * bouncinessFactor;
        velocity.y = 0;
        _velocityFactor = 0;
        AddForce(reaction);
        _velocityFactor = 1;
    }

    private void ReactionToCollisionStay()
    {
        AddForce(new Vector3(0, physicsProperties.gravity, 0));
        if (Mathf.Abs(velocity.z) > 0)
        {
            var opposeForce = GetDirection(velocity.z) * physicsProperties.friction;
            AddForce(new Vector3(0, physicsProperties.gravity, -opposeForce));
        }
        else
        {
            velocity.z = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!_movementStarted) return;
        
        var freezePosX = bodyConstraints.freezePositionX ? 0 : 1;
        var freezePosY = bodyConstraints.freezePositionY ? 0 : 1;
        var freezePosZ = bodyConstraints.freezePositionZ ? 0 : 1;
        acceleration.y -= physicsProperties.gravity;
        velocity += acceleration * (_velocityFactor * Time.fixedDeltaTime);
        var displacement = velocity * Time.fixedDeltaTime;
        
        transform.Translate(displacement, Space.World);
        Rotate();

        acceleration = Vector3.zero;
    }
    
    private void Rotate()
    {
        if (_wingsOpen)
        {
            rocketman.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            rocketman.transform.Rotate(new Vector3(velocity.z * rotationFactor, 0, 0) * Time.fixedDeltaTime);
        }
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
