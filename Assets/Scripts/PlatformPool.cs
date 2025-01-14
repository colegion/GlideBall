using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using static Helpers.Utilities;

public class PlatformPool : MonoBehaviour
{
    [SerializeField] private int poolAmount;

    private Dictionary<PlatformType, Queue<IPoolable>> _pooledPlatforms;

    private void OnEnable()
    {
        //InitializePlatformPool();
    }

    public void InitializePlatformPool()
    {
        _pooledPlatforms = new Dictionary<PlatformType, Queue<IPoolable>>();
        
        foreach (PlatformType platformType in Enum.GetValues(typeof(PlatformType)))
        {
            Queue<IPoolable> platformQueue = new Queue<IPoolable>();
            Platform prefab = Resources.Load<Platform>($"Prefabs/{platformType}");

            if (prefab == null)
            {
                Debug.LogWarning($"Prefab for {platformType} not found.");
                continue;
            }

            for (int i = 0; i < poolAmount; i++)
            {
                Platform instance = Instantiate(prefab, transform);
                instance.OnPooled(this);
                platformQueue.Enqueue(instance);
            }

            _pooledPlatforms[platformType] = platformQueue;
        }
    }

    public IPoolable GetPlatformByType(PlatformType type)
    {
        if (_pooledPlatforms.TryGetValue(type, out Queue<IPoolable> pool) && pool.Count > 0)
        {
            IPoolable platform = pool.Dequeue();
            return platform;
        }

        Debug.LogWarning($"No available type {type} in the pool.");
        return null;
    }

    public void ReturnPlatform(Platform platform)
    {
        if (_pooledPlatforms.TryGetValue(platform.Type, out Queue<IPoolable> pool))
        {
            platform.OnReturnPool();
            pool.Enqueue(platform);
        }
        else
        {
            Debug.LogError($"{platform.Type} does not exist in the pool.");
        }
    }
}