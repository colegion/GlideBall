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

    public PlatformType Type => _type;

    public void ConfigureSelf(Vector3 position, PlatformType type)
    {
        _type = type;
        transform.position = position;
    }

    public void OnFetchedFromPool()
    {
        visuals.gameObject.SetActive(true);
    }

    public void ResetSelf()
    {
        transform.position = Vector3.zero;
        visuals.gameObject.SetActive(false);
    }
}
