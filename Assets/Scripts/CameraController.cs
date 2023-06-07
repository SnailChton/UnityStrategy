using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed = 10.0f, cameraSpeed = 10.0f, zoomSpeed = 800.0f;
    private float _mult = 1f;

    private float horizontal;
    private float vertical;

    private void Update() {
        // Rotation
        float rotate = 0f;
        if (Input.GetKey(KeyCode.Q)) {
            rotate = - 1f;
        } else if (Input.GetKey(KeyCode.E)) {
            rotate = 1f;
        }
    
        _mult = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * _mult * rotate, Space.World );

        // Move Camera

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, 0, vertical) * Time.deltaTime * _mult * cameraSpeed, Space.Self);

        // Zoom 

        transform.position += transform.up * zoomSpeed * Time.deltaTime * -Input.GetAxis("Mouse ScrollWheel");

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -14f, 30f), transform.position.z);
    }

}
