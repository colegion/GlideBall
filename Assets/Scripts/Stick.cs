using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Stick : MonoBehaviour
{
    [SerializeField] private Transform playerInitialTranform;
    [SerializeField] private Transform[] bones;
    [SerializeField] private Animator stickAnimator;
    [SerializeField] private float sensitivity;
    private float _lastTilt;

    private static int _releaseAnimationKey = Animator.StringToHash("StickRelease");
    public void TiltBones(float angle)
    {
        if (stickAnimator.enabled)
            stickAnimator.enabled = false;
        _lastTilt = (angle / Screen.width) * sensitivity;
        if (_lastTilt < 0) _lastTilt = 0;
        foreach (var bone in bones)
        {
            bone.transform.localRotation = Quaternion.Euler(new Vector3(-_lastTilt, 0, 0));
        }
    }

    public void PlayReleaseAnimation()
    {
        stickAnimator.enabled = true;
        stickAnimator.Play(_releaseAnimationKey);
    }

    public float GetLastTilt()
    {
        return _lastTilt;
    }

    public void RePositionPlayer(Player player)
    {
        _lastTilt = 0;
        player.transform.SetParent(transform);
        player.transform.localPosition = playerInitialTranform.position;
    }
}
