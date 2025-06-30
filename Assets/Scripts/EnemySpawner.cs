using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawData
{
    public PatrolArea area;
    public List<SpawnEnemy> enemiesToSpawn;
}

[Serializable]
public class SpawnEnemy
{
    public EnemyType type;
    public List<Transform> spawnPoints;
}

public class EnemySpawner : MonoBehaviour
{
    public InUseEnemy inUseEnemy;
    public List<SpawData> spawData;
    
    public void SpawnEnemies()
    {
        foreach (var data in spawData)
        {
            foreach (var enemy in data.enemiesToSpawn)
            {
                foreach (var spawnPoint in enemy.spawnPoints)
                {
                    var spawnObj = Instantiate(inUseEnemy.GetEnemyByType(enemy.type), spawnPoint.position, Quaternion.identity);
                    spawnObj.transform.SetParent(data.area.transform);
                    spawnObj.GetComponent<Enemy>().SetPatrolArea(data.area);
                }
            }
        }
    }
}