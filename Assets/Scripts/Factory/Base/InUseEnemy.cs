using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InUseEnemy", menuName = "Data Object/InUseEnemy")]
public class InUseEnemy : ScriptableObject
{
    [Serializable]
    public class EnemyEntry
    {
        public EnemyType enemyType;
        public GameObject enemyPrefab;
    }
    
    public List<EnemyEntry> enemies = new List<EnemyEntry>();
    
    public GameObject GetEnemyByType(EnemyType type)
    {
        foreach (var enemyEntry in enemies)
        {
            if (enemyEntry.enemyType == type)
                return enemyEntry.enemyPrefab;
        }

        Debug.LogError($"Enemy with type {type} not found!");
        return null;
    }
}