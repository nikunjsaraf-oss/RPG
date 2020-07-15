using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
    }
    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    // Returns a ray going from camera through a screen point.
        RaycastHit hit;         // Creates a ray starting at origin along direction.
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            GetComponent<Mover>().MoveTo(hit.point);
        }
    }
}
