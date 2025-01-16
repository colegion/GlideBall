using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private float duration;
    private Transform _targetToFollow;
    private bool _followingStarted;
    
    public void SetTarget(Transform target, Action onComplete)
    {
        _targetToFollow = target;
        AnimateInitialMove(() =>
        {
            onComplete?.Invoke();
        });
    }

    private void AnimateInitialMove(Action onComplete)
    {
        transform.DOMove(_targetToFollow.position + Vector3.back * 10f, duration).SetEase(movementCurve).OnComplete(
            () =>
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
                onComplete?.Invoke();
                _followingStarted = true;
            });
    }

    private void FixedUpdate()
    {
        if (!_followingStarted) return;
        
        transform.position = _targetToFollow.position + Vector3.back * 10f;
    }
}

