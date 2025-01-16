using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float targetDistance;
    private Transform _target;

    public void FollowTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        if (_target == null) return;

        transform.position = _target.transform.position - Vector3.back * targetDistance;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
