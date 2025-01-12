using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private InUseEnemy inUseEnemy;
    
    [Header("Pool setting")]
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxPoolSize = 100;
    
    private Dictionary<EnemyType, ObjectPool<GameObject>> _enemyPools = new Dictionary<EnemyType, ObjectPool<GameObject>>();
    private Dictionary<EnemyType, Transform> _enemyPoolTransforms = new Dictionary<EnemyType, Transform>();

    private void Awake()
    {
        foreach (var enemy in inUseEnemy.enemies)
        {
            CreatePool(enemy.enemyType, enemy.enemyPrefab);
        }
    }
    
    private void CreatePool(EnemyType enemyType, GameObject enemyPrefab)
    {
        // Create a parent for the pooled objects for organization
        Transform poolParent = new GameObject($"{enemyType}_Pool").transform;
        poolParent.SetParent(transform);
        _enemyPoolTransforms[enemyType] = poolParent;

        // Create an ObjectPool for this enemy type
        var pool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject obj = Instantiate(enemyPrefab);
                obj.SetActive(false);
                obj.transform.SetParent(poolParent);
                return obj;
            },
            actionOnGet: obj => obj.SetActive(true),
            actionOnRelease: obj =>
            {
                obj.SetActive(false);
                obj.transform.SetParent(poolParent); // Return to pool parent
            },
            actionOnDestroy: Destroy,
            defaultCapacity: defaultCapacity,
            maxSize: maxPoolSize
        );

        _enemyPools[enemyType] = pool;
    }
    
    // Func
    public object SpawnEnemy((EnemyType enemyType, Vector3 position) parameters)
    {
        if (!_enemyPools.ContainsKey(parameters.enemyType))
        {
            Debug.LogError($"No pool found for enemy type: {parameters.enemyType}");
            return null;
        }

        // Get an enemy from the pool
        GameObject enemy = _enemyPools[parameters.enemyType].Get();
        enemy.transform.position = parameters.position;
        enemy.transform.rotation = Quaternion.identity;
        return null;
    }
    
    // Func
    public object ReleaseEnemy((EnemyType enemyType, GameObject enemy) parameters)
    {
        if (!_enemyPools.ContainsKey(parameters.enemyType))
        {
            Debug.LogError($"No pool found for enemy type: {parameters.enemyType}");
            return null;
        }

        // Release the enemy back to the pool
        _enemyPools[parameters.enemyType].Release(parameters.enemy);

        return null;
    }
}