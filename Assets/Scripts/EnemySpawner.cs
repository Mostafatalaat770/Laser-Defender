using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;
    WaveConfig currentWave;
    [SerializeField] bool looping = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++) {
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[waveIndex]));
                }
    }
    IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        int numberOfEnemies = 0;
        while (numberOfEnemies++ < currentWave.NumberOfEnemies)
        {
            var newEnemy = Instantiate(currentWave.EnemyPrefab,
                currentWave.getWaypoints()[0].position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().setWaveConfig(currentWave);
            yield return new WaitForSeconds(currentWave.TimeBetweenSpawns);
        }

    }
}
