using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float spawnRate = 1;
    private SpawnPoint[] spawnPoints;
    public GameObject[] ennemiPrefab;
    static List<GameObject> Ennemis;
    private float timeLeftBeforeSpawn = 0;
    public int MaxEnnemiInSpawn;
    // Use this for initialization
    void Start()
    {
        spawnPoints = FindObjectsOfType<SpawnPoint>();

        timeLeftBeforeSpawn = 1 / spawnRate;
        Ennemis = spawnPoints[0].Ennemis;
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
        if (Ennemis.Count < MaxEnnemiInSpawn)
        {
            int countSpawnPoint = spawnPoints.Length;
            int randomSpawnPointIndex = Random.Range(0, countSpawnPoint);
            SpawnPoint spawnpointRandomlySelected = spawnPoints[randomSpawnPointIndex];
            GameObject newEnemy = Instantiate(ennemiPrefab[Random.Range(0, ennemiPrefab.Length)], spawnpointRandomlySelected.GetPosition(), spawnpointRandomlySelected.transform.rotation);
            Ennemis.Add(newEnemy);
        }


    }
    public void Remove(GameObject Enemy)
    {
        Ennemis.Remove(Enemy);
    }
}
