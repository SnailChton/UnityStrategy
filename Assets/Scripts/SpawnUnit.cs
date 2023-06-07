using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    public GameObject unit;
    public float time = 20f;

    private void Start() {

        StartCoroutine(SpawnUnits());
    }

    IEnumerator SpawnUnits () {

        for (int i = 0; i <= 3; i++) {
            yield return new WaitForSeconds(time);

            Vector3 spawnPosition = new Vector3(
                transform.GetChild(0).position.x + UnityEngine.Random.Range(2f, 5f), 
                transform.GetChild(0).position.y, 
                transform.GetChild(0).position.z + UnityEngine.Random.Range(2f, 5f));

            Instantiate(unit, spawnPosition, Quaternion.identity);
        }
        
    }
}
