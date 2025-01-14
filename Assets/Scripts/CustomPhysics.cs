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
    
    public void AddForce(Vector3 force)
    {
        velocity = force / mass;
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(velocity.x * Convert.ToInt16(bodyConstraints.freezePositionX), 
            (velocity.y - physicsProperties.gravity) * Convert.ToInt16(bodyConstraints.freezePositionY), 
            velocity.z * Convert.ToInt16(bodyConstraints.freezePositionZ)) * Time.fixedTime;
    }
}
