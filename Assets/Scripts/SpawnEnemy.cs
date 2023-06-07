using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public Transform[] enemySpawnPoints;
    public GameObject enemyBaraks;
    
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    
    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemySpawnPoints.Length; i++) {
            
            yield return new WaitForSeconds(20f);
            GameObject spawn = Instantiate(enemyBaraks);
            spawn.transform.position = enemySpawnPoints[i].position + new Vector3(0, 0.8f, 0);
            spawn.transform.rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
            spawn.GetComponent<SpawnUnit>().enabled = true;
        }
    }
}
