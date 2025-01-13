using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Interfaces;
using UnityEngine;
using static Helpers.Utilities;

public class Platform : MonoBehaviour, IPoolable
{
    [SerializeField] private GameObject visuals;
    private PlatformType _type;
    private PlatformPool _controller;

    public PlatformType Type => _type;
    
    public void ConfigureSelf(Vector3 position, PlatformType type)
    {
        _type = type;
        transform.position = position;
    }

    public void OnPooled(PlatformPool controller)
    {
        _controller = controller;
    }

    public void OnFetchedFromPool()
    {
        visuals.gameObject.SetActive(true);
    }

    public void OnReturnPool()
    {
        transform.position = Vector3.zero;
        visuals.gameObject.SetActive(false);
    }

    public GameObject GameObject()
    {
        return gameObject;
    }
}
