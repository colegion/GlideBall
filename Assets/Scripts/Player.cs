using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject rocketman;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CustomPhysics physics;

    private bool _wingsEnabled;
    private static readonly int IsWingsOpen = Animator.StringToHash("isWingsOpen");

    public void HandleOnStickReleased(float tilt)
    {
        physics.AddForce(new Vector3(0, tilt, tilt * 3f));
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
            physics.AddForce(new Vector3(0, platform.GetBoostAmount(), 0));
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
