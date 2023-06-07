using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public GameObject cube;
    public LayerMask layer, unitsLayer;
    public List<GameObject> selectedUnits;
    private Camera _cam;
    private GameObject _cubeSelection;
    private RaycastHit _hit;


    private void Awake() {
        _cam = GetComponent<Camera>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            selectedUnits.Clear();
            
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, 1000f, layer)) {
                
                _cubeSelection = Instantiate(cube, new Vector3(_hit.point.x, 1, _hit.point.z), Quaternion.identity);
            }
        }

        if (_cubeSelection) {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer)) {
                float xScale = _hit.point.x - hitDrag.point.x;
                float zScale = _hit.point.z - hitDrag.point.z;

                if (xScale < 0.0f && zScale < 0.0f) {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }

                _cubeSelection.transform.localScale = new Vector3(xScale, 1, zScale);
            }
        }

        if (Input.GetMouseButtonUp(0) && _cubeSelection) {
            
            RaycastHit[] hits = Physics.BoxCastAll(
                _cubeSelection.transform.position,
                _cubeSelection.transform.localScale,
                Vector3.up,
                Quaternion.identity,
                0,
                unitsLayer);

            foreach (var el in hits) {
                selectedUnits.Add(el.transform.gameObject);
            }
            
            Destroy(_cubeSelection);

        }
    }
}
