using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public LayerMask layer;
    public float rotateSpeed = 20f;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, layer)) {
            transform.position = hit.point;
            transform.position = transform.position + new Vector3(0, 1.8f, 0);
        }

        if (Input.GetKey(KeyCode.Z)) {
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        } else if (Input.GetKey(KeyCode.X)) {
            transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed);
        }

        if (Input.GetMouseButtonDown(0)) {
            if (gameObject.GetComponent<SpawnUnit>()) {
                gameObject.GetComponent<SpawnUnit>().enabled = true;
            }
            
            Destroy(gameObject.GetComponent<PlaceObjects>());
        }
    }
}
