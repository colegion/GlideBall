using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Scriptables;
using UnityEngine;

public class CustomPhysics : MonoBehaviour
{
    [SerializeField] private CustomPhysicsProperties physicsProperties;
    [SerializeField] private Utilities.Constraints bodyConstraints;
    
    public float mass;
    public Vector3 velocity;
    private bool _movementStarted;
    
    public void AddForce(Vector3 force)
    {
        _movementStarted = true;
        velocity = force / mass;
        Debug.Log("velocity is :" + velocity);
    }

    private void FixedUpdate()
    {
        if (!_movementStarted) return;
        var inversedX = Convert.ToInt16(bodyConstraints.freezePositionX) == 0 ? 1 : 0;
        var inversedY = Convert.ToInt16(bodyConstraints.freezePositionY) == 0 ? 1 : 0;
        var inversedZ = Convert.ToInt16(bodyConstraints.freezePositionZ) == 0 ? 1 : 0;
        transform.position += new Vector3(velocity.x * inversedX, 
            (velocity.y - physicsProperties.gravity) * inversedY, 
            velocity.z * inversedZ) * Time.fixedTime;
        Debug.Log("position: " + transform.position);
    }
}
