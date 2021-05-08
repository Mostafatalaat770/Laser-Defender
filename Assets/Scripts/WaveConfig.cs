using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Wave", fileName ="Wave")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.3f;
    [SerializeField] float spawnRandomRate = 0.5f;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] int numberOfEnemies = 5;

    public GameObject EnemyPrefab { get => enemyPrefab;}
    public List<Transform> getWaypoints()
    {
        var waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }
    public float TimeBetweenSpawns { get => timeBetweenSpawns;}
    public float SpawnRandomRate { get => spawnRandomRate;}
    public float MoveSpeed { get => moveSpeed;}
    public int NumberOfEnemies { get => numberOfEnemies;}
}
