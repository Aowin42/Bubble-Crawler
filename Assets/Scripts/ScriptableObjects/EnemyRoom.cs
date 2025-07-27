using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Configuration")]
public class EnemyRoom : ScriptableObject
{
    /// <Summary>
    /// Should contain the information on which enemies are being spawned 
    /// which is then read by the enemySpawner class
    /// <Summary>
    
    [System.Serializable]
    public struct EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public int count;
    }

    public List<EnemySpawnInfo> enemiesToSpawn;
}
