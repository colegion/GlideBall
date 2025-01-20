using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float upwardMoveFactor;
    [SerializeField] private float forwardMoveFactor;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CustomPhysics physics;

    private bool _acceptInput;
    private bool _wingsEnabled;
    private static readonly int IsWingsOpen = Animator.StringToHash("isWingsOpen");
    
    public void HandleOnStickReleased(float tilt)
    {
        physics.AddForce(new Vector3(0, tilt * upwardMoveFactor, tilt * forwardMoveFactor));
    }

    public void ToggleWings(bool toggle)
    {
        if (toggle)
        {
            if (!_wingsEnabled)
            {
                _wingsEnabled = true;
                playerAnimator.SetBool(IsWingsOpen, true);
                physics.SetCanRotate(true);
            }
        }
        else
        {
            if (_wingsEnabled)
            {
                _wingsEnabled = false;
                playerAnimator.SetBool(IsWingsOpen, false);
                physics.SetCanRotate(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Platform platform))
        {
            Debug.Log("trigger with platform :" + platform.name + " " + platform.GetBoostAmount());
            Vector3 platformNormal = other.transform.up;
            float boostAmount = platform.GetBoostAmount();
            
            Vector3 boostForce = platformNormal * boostAmount;
            physics.AddForce(boostForce);
        }
    }

    public void TiltByAmount(Vector2 amount)
    {
        physics.SetIsGliding(true);
        physics.SetGlideAmount(amount.x);
    }

    public void DisableGliding()
    {
        physics.SetIsGliding(false);
        physics.SetGlideAmount(0);
    }
}
