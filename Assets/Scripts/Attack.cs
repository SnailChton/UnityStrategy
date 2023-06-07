using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public int _health = 100;
    public float radius = 30f;
    public GameObject arrow;
    private Coroutine _coroutine;

    void Update () {
        DetectCollision();
    }

    private void DetectCollision() {

        Collider[] inRadiusColliders = Physics.OverlapSphere(transform.position, radius);

        if (inRadiusColliders.Length == 0 && _coroutine != null) {

            StopCoroutine(_coroutine);
            _coroutine = null;

            if (gameObject.CompareTag("Enemy")) {

                GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(gameObject.transform.position);
            }
        }

        

        foreach (var element in inRadiusColliders) {

            if ((gameObject.CompareTag("Unit") && element.gameObject.CompareTag("Enemy")) || (gameObject.CompareTag("Enemy") && element.gameObject.CompareTag("Unit"))) {

                if (gameObject.CompareTag("Enemy")) {

                    GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(element.transform.position);
                }

                if (_coroutine == null) {

                    _coroutine = StartCoroutine(StartAttack(element));
                }
            }
        }
    }
    
    IEnumerator StartAttack(Collider enemyPosition) {

        GameObject createdArrow = Instantiate(arrow, transform.GetChild(1).position, Quaternion.identity);
        createdArrow.GetComponent<ArrowShooter>().position = enemyPosition.transform.position;
        yield return new WaitForSeconds(3f);
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
