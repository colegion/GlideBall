using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private float duration;
    [SerializeField] private float backwardFollowingDistance;
    [SerializeField] private float upwardFollowingDistance;
    private Transform _targetToFollow;
    private bool _followingStarted;
    private Vector3 _fixedPos;
    
    public void SetTarget(Transform target, Action onComplete)
    {
        _targetToFollow = target;
        _fixedPos = _targetToFollow.transform.position + Vector3.back * backwardFollowingDistance +
                        Vector3.up * upwardFollowingDistance; 
        AnimateInitialMove(onComplete);
    }

    private void AnimateInitialMove(Action onComplete)
    {
        // Create a DOTween sequence
        Sequence sequence = DOTween.Sequence();
        
        // Add movement to the sequence
        sequence.Append(transform.DOMove(_fixedPos, duration)
            .SetEase(movementCurve));

        // Add rotation to the sequence in parallel with movement
        sequence.Join(transform.DORotate(new Vector3(26, 0, 0), duration).SetEase(movementCurve));

        // Add completion callback
        sequence.OnComplete(() =>
        {
            _followingStarted = true;
        });
        onComplete?.Invoke();
    }

    private void FixedUpdate()
    {
        if (!_followingStarted) return;
        var followingPos =  _targetToFollow.transform.position + Vector3.back * backwardFollowingDistance +
                            Vector3.up * upwardFollowingDistance; 
        transform.position = followingPos;
    }

}

