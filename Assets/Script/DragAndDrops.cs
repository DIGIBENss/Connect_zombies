using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrops : MonoBehaviour
{
    
    private Vector3 _offset;
    private Plane _dragPlane;
    private Camera _myCamera;
   

    private void Awake()
    {
        _myCamera = Camera.main;
    }
    private void OnMouseDown()
    {
        _dragPlane = new Plane(_myCamera.transform.forward, transform.position);
        Ray camRay = _myCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        _dragPlane.Raycast(camRay, out planeDist);
        _offset = transform.position - camRay.GetPoint(planeDist);


    }
    private void OnMouseDrag()
    {
        Ray camRay = _myCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        _dragPlane.Raycast(camRay, out planeDist);
        transform.position = camRay.GetPoint(planeDist) + _offset;
    }

}
