using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public Vector3 position;
    public float speed = 30f; 
    public int damage = 20;

    private void Update() {

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);

        if (transform.position == position) {

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("Enemy") || other.CompareTag("Unit")) {

            Attack attack = other.GetComponent<Attack>();

            attack._health -= damage;

            Transform hp = other.transform.GetChild(0).transform;
            hp.localScale = new Vector3(
                hp.localScale.x - 0.4f,
                hp.localScale.y,
                hp.localScale.z
            );

            if (attack._health <= 0) {
                Destroy(other.gameObject);
            }
        }
    }
}
