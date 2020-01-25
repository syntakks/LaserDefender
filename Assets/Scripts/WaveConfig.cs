using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject EnemyPrefab { get => enemyPrefab; }
    public GameObject PathPrefab { get => pathPrefab; }
    public float TimeBetweenSpawns { get => timeBetweenSpawns; }
    public float SpawnRandomFactor { get => spawnRandomFactor; }
    public int NumberOfEnemies { get => numberOfEnemies; }
    public float MoveSpeed { get => moveSpeed; }

    public List<Transform> GetWaypoints()
    {
        var waveWayopints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWayopints.Add(child); 
        }
        return waveWayopints; 
    }
}
