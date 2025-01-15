using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CustomPhysics physics;

    private static int _openWings = Animator.StringToHash("OpenWings");
    private static int _closeWings = Animator.StringToHash("CloseWings");

    private bool _wingsEnabled;
    
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
        _wingsEnabled = toggle;
        if (toggle)
        {
            playerAnimator.Play(_openWings);
        }
        else
        {
            playerAnimator.Play(_closeWings);
        }
    }
    
    private void AddListeners()
    {
    }

    private void RemoveListeners()
    {
    }
}
