using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform stickTarget;

    private void Update()
    {
        transform.position = stickTarget.position;
        transform.rotation = stickTarget.rotation;
    }
}
