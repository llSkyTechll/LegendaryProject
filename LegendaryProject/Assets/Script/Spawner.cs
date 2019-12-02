using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRate = 1;
    private SpawnPoint[] spawnPoints;
    public GameObject[] ennemiPrefab;
    private float timeLeftBeforeSpawn = 0;
    public int MaxEnnemiInSpawn;
    private int count = 0;
    // Use this for initialization
    void Start()
    {
        spawnPoints = FindObjectsOfType<SpawnPoint>();
        timeLeftBeforeSpawn = 1 / spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSapwn();
    }
    private void UpdateSapwn()
    {
        timeLeftBeforeSpawn -= Time.deltaTime;
        if (timeLeftBeforeSpawn < 0)
        {
            SpawnEnnemi();
            timeLeftBeforeSpawn = 1 / spawnRate;
        }
    }
    private void SpawnEnnemi()
    {
        int countSpawnPoint = spawnPoints.Length;
        int randomSpawnPointIndex = Random.Range(0, countSpawnPoint);
        SpawnPoint spawnpointRandomlySelected = spawnPoints[randomSpawnPointIndex];

        if (spawnpointRandomlySelected.CountEnemyInList() < MaxEnnemiInSpawn)
        {
            GameObject newEnemy = Instantiate(ennemiPrefab[0/*Random.Range(0, ennemiPrefab.Length)*/], spawnpointRandomlySelected.GetPosition(), spawnpointRandomlySelected.transform.rotation);
            newEnemy.GetComponentInChildren<GhostTiger>().spawnId = spawnpointRandomlySelected.idSpawn;
            newEnemy.GetComponentInChildren<GhostTiger>().Id = count;
            count++;
            spawnpointRandomlySelected.AddEnnemi(newEnemy);
        }
    }
    public void Remove(GameObject inEnemy)
    {
        foreach (SpawnPoint spawn in spawnPoints)
        {
            if (spawn.idSpawn == inEnemy.GetComponentInChildren<GhostTiger>().spawnId)
            {
                spawn.Remove(inEnemy);
            }
        }
    }
}
