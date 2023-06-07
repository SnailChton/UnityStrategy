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
    
        // move

        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0) {

            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit moveTo, 1000f, layer)) {

                foreach (var element in selectedUnits) {

                    if (element != null) {

                        element.transform.GetChild(0).gameObject.SetActive(false);
                    }

                    element.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(moveTo.point);
                }

            }
        }

        // new selection field 

        if (Input.GetMouseButtonDown(0)) {

            // clear previous
            foreach (var element in selectedUnits) {

                if (element != null) {
                    
                    element.transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            selectedUnits.Clear();
            
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, 1000f, layer)) {
                
                _cubeSelection = Instantiate(cube, new Vector3(_hit.point.x, 1, _hit.point.z), Quaternion.identity);
            }
        }

        // selection field scale
        if (_cubeSelection) {

            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer)) {
                float xScale = (_hit.point.x - hitDrag.point.x) * -1;
                float zScale = (_hit.point.z - hitDrag.point.z) * -1;

                if (xScale < 0.0f && zScale < 0.0f) {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                } else if (xScale < 0.0f) {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                } else if (zScale < 0.0f) {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
                } else _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));


                _cubeSelection.transform.localScale = new Vector3(Mathf.Abs(xScale), 1, Mathf.Abs(zScale)); 
            }
        }

        // selected

        if (Input.GetMouseButtonUp(0) && _cubeSelection) {
            
            // return units in range of selection field
            RaycastHit[] hits = Physics.BoxCastAll(
                _cubeSelection.transform.position,
                _cubeSelection.transform.localScale,
                Vector3.up,
                Quaternion.identity,
                0,
                unitsLayer);

            foreach (var element in hits) {
                // selected units to list
                selectedUnits.Add(element.transform.gameObject);
                // hp bar
                element.transform.GetChild(0).gameObject.SetActive(true);
            }
            // delete selection field
            Destroy(_cubeSelection);

        }
    }
}
