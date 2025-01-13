using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using static Helpers.Utilities;

public class PlatformController : MonoBehaviour
{
    private Dictionary<Vector2Int, List<Vector3>> _spatialHash = new Dictionary<Vector2Int, List<Vector3>>();
    private readonly float _cellSize = 5f;
    private readonly float _minDistance = 20f;

    [SerializeField] private PlatformPool platformPool;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnDistance = 50f;
    [SerializeField] private float despawnDistance = -10f;
    private List<Vector3> _spawnPoints;

    private readonly List<Platform> _activePlatforms = new List<Platform>();

    private void ManagePlatforms()
    {
        while (NeedsMorePlatforms())
        {
            Vector3 spawnPoint = GetRandomSpawnPoint();
            PlatformType type = GetRandomPlatformType();
            IPoolable platform = platformPool.GetPlatformByType(type);

            if (platform.GameObject().TryGetComponent(out Platform temp))
            {
                temp.ConfigureSelf(spawnPoint, type);
                _activePlatforms.Add(temp);
            }
        }
        
        for (int i = _activePlatforms.Count - 1; i >= 0; i--)
        {
            Platform platform = _activePlatforms[i];
            if (platform.transform.position.z < player.position.z + despawnDistance)
            {
                platformPool.ReturnPlatform(platform);
                _activePlatforms.RemoveAt(i);
            }
        }
    }

    private bool NeedsMorePlatforms()
    {
        foreach (var platform in _activePlatforms)
        {
            if (platform.transform.position.z > player.position.z + spawnDistance)
            {
                return false;
            }
        }
        return true;
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
            Vector3 newPoint = new Vector3(randomX, 0, randomZ);
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

        foreach (var cell in cellsToRemove)
        {
            _spatialHash.Remove(cell);
        }
    }

    private Vector2Int GetCell(Vector3 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / _cellSize),
            Mathf.FloorToInt(position.z / _cellSize)
        );
    }
    
    private PlatformType GetRandomPlatformType()
    {
        return (PlatformType)Random.Range(0, System.Enum.GetValues(typeof(PlatformType)).Length);
    }
}
