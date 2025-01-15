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

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    public void HandleOnStickReleased(float tilt)
    {
        physics.AddForce(new Vector3(tilt, 0, 0));
    }

    public void ToggleWings(bool toggle)
    {
        if (toggle)
        {
            if (!_wingsEnabled)
            {
                _wingsEnabled = true;
                playerAnimator.SetBool(IsWingsOpen, true);
            }
        }
        else
        {
            if (_wingsEnabled)
            {
                _wingsEnabled = false;
                playerAnimator.SetBool(IsWingsOpen, false);
            }
        }
    }
    
    private void AddListeners()
    {
    }

    private void RemoveListeners()
    {
    }
}
