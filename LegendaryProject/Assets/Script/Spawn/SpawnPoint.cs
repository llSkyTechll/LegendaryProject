using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SpawnPoint : MonoBehaviour
{
    private List<GameObject> enemys = new List<GameObject>();
    public int idSpawn = 0;

    public Vector3 GetPosition()
    {
        if (SceneManager.GetActiveScene().name != "Labyrinth")
        {
            return gameObject.transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        }
        return gameObject.transform.position;
    }

    public void Remove(GameObject ennemyToRemove)
    {
        GameObject itemToRemove = enemys.Where(r => r.GetInstanceID() == ennemyToRemove.GetInstanceID()).First();
        if (itemToRemove != null)
        {
            enemys.Remove(itemToRemove);
        }
    }
    public void AddEnnemi(GameObject newEnemy)
    {
        enemys.Add(newEnemy);
    }
    public int CountEnemyInList()
    {
        return enemys.Count;
    }
    public List<GameObject> GetListEnemy()
    {
        return enemys;
    }
}
