using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using static Helpers.Utilities;
using Random = UnityEngine.Random;

public class PlatformController : MonoBehaviour
{
    private Dictionary<Vector2Int, List<Vector3>> _spatialHash = new Dictionary<Vector2Int, List<Vector3>>();
    private readonly float _cellSize = 10f;
    private readonly float _minDistance = 25f;

    [SerializeField] private PlatformPool platformPool;
    [SerializeField] private Transform player;
    [SerializeField] private float ySpawnPosition;
    [SerializeField] private float spawnDistance;
    [SerializeField] private float despawnDistance;
    private List<Vector3> _spawnPoints;
    private bool _poolReady;
    private bool _initialSpawnHandled;
    private bool _movementStarted;
    private readonly List<Platform> _activePlatforms = new List<Platform>();

    private void Start()
    {
        _poolReady = platformPool.InitializePlatformPool();
    }

    private void FixedUpdate()
    {
        if (!_poolReady) return;
        if (!_initialSpawnHandled)
        {
            SpawnPlatformsIfNeeded();
            _initialSpawnHandled = true;
        }

        if (_movementStarted)
            ManagePlatforms();
    }
    
    private void ManagePlatforms()
    {
        RemoveOutOfViewPlatforms();
        SpawnPlatformsIfNeeded();
    }

    private void SpawnPlatformsIfNeeded()
    {
        float spawnThreshold = player.position.z + spawnDistance;
        if (_spatialHash.Count > 0)
        {
            var lastRow = GetLastRowInSpatialHash();
            if (lastRow != null && lastRow.Count > 0)
            {
                float lastPlatformZ = lastRow[^1].z;
                if (lastPlatformZ > spawnThreshold)
                {
                    return;
                }
            }
        }

        for (int i = 0; i < 20; i++)
        {
            Vector3 spawnPoint = GetRandomSpawnPoint();
            if (spawnPoint == Vector3.zero) return;
            PlatformType type = GetRandomPlatformType();
            IPoolable platform = platformPool.GetPlatformByType(type);

            if (platform == null)
            {
                Debug.LogWarning($"No available platforms of type {type} in the pool.");
                return;
            }

            if (platform.GameObject().TryGetComponent(out Platform temp))
            {
                temp.OnFetchedFromPool();
                temp.ConfigureSelf(spawnPoint, type);
                _activePlatforms.Add(temp);
            }
        }
    }

    private List<Vector3> GetLastRowInSpatialHash()
    {
        Vector2Int lastKey = default;
        bool hasKey = false;

        foreach (var key in _spatialHash.Keys)
        {
            if (!hasKey || key.y > lastKey.y)
            {
                lastKey = key;
                hasKey = true;
            }
        }

        return hasKey && _spatialHash.TryGetValue(lastKey, out var row) ? row : null;
    }


    private Vector3 GetRandomSpawnPoint()
    {
        Camera camera = Camera.main;
        if (camera == null) return Vector3.zero;

        Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, spawnDistance));
        Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, spawnDistance));

        for (int attempt = 0; attempt < 100; attempt++)
        {
            float randomX = Random.Range(bottomLeft.x, topRight.x);
            float randomZ = Random.Range(bottomLeft.z, topRight.z);
            Vector3 newPoint = new Vector3(randomX, ySpawnPosition, randomZ);
            Vector2Int cell = GetCell(newPoint);
            if (IsPointValid(cell, newPoint))
            {
                if (!_spatialHash.ContainsKey(cell))
                    _spatialHash[cell] = new List<Vector3>();
                _spatialHash[cell].Add(newPoint);

                return newPoint;
            }
        }

        Debug.LogWarning("Could not find a unique spawn point.");
        return Vector3.zero;
    }

    private bool IsPointValid(Vector2Int cell, Vector3 point)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                Vector2Int neighborCell = new Vector2Int(cell.x + x, cell.y + z);
                if (_spatialHash.TryGetValue(neighborCell, out List<Vector3> points))
                {
                    foreach (Vector3 existingPoint in points)
                    {
                        if (Vector3.Distance(point, existingPoint) < _minDistance)
                            return false;
                    }
                }
            }
        }

        return true;
    }

    private void RemoveOutOfViewPlatforms()
    {
        float despawnThreshold = player.position.z + despawnDistance;
        for (int i = _activePlatforms.Count - 1; i >= 0; i--)
        {
            Platform platform = _activePlatforms[i];
            if (platform.transform.position.z < despawnThreshold)
            {
                platformPool.ReturnPlatform(platform);
                _activePlatforms.RemoveAt(i);
            }
        }

        RemoveOutOfViewPoints();
    }

    private void RemoveOutOfViewPoints()
    {
        Camera camera = Camera.main;
        if (camera == null) return;

        Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, spawnDistance));
        Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, spawnDistance));
        Vector2Int minCell = GetCell(bottomLeft);
        Vector2Int maxCell = GetCell(topRight);
        List<Vector2Int> cellsToRemove = new List<Vector2Int>();
        foreach (var cell in _spatialHash.Keys)
        {
            if (cell.x < minCell.x || cell.x > maxCell.x || cell.y < minCell.y || cell.y > maxCell.y)
            {
                cellsToRemove.Add(cell);
            }
        }
    }

    public void ResetPool()
    {
        foreach (var platform in _activePlatforms)
        {
            platformPool.ReturnPlatform(platform);
        }
        _activePlatforms.Clear();
        _spatialHash.Clear();
        _initialSpawnHandled = false;
        _movementStarted = false;

        Debug.Log("Platform pool reset successfully.");
    }


    private Vector2Int GetCell(Vector3 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / _cellSize),
            Mathf.FloorToInt(position.z / _cellSize)
        );
    }

    public void SetMovementStarted(bool value)
    {
        _movementStarted = value;
    }

    private PlatformType GetRandomPlatformType()
    {
        return (PlatformType)Random.Range(0, Enum.GetValues(typeof(PlatformType)).Length);
    }
}