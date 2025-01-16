using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CustomPhysics physics;

    private bool _wingsEnabled;
    private static readonly int IsWingsOpen = Animator.StringToHash("isWingsOpen");

    public void HandleOnStickReleased(float tilt)
    {
        physics.AddForce(new Vector3(0, tilt, tilt * 500f));
    }

    public void ToggleWings(bool toggle)
    {
        if (toggle)
        {
            if (!_wingsEnabled)
            {
                _wingsEnabled = true;
                playerAnimator.SetBool(IsWingsOpen, true);
                physics.SetWingsStatus(true);
            }
        }
        else
        {
            if (_wingsEnabled)
            {
                _wingsEnabled = false;
                playerAnimator.SetBool(IsWingsOpen, false);
                physics.SetWingsStatus(false);
            }
        }
    }

    public void TiltByAmount(Vector2 amount)
    {
        var initialRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, amount.x, 0) + initialRotation);
    }
}
