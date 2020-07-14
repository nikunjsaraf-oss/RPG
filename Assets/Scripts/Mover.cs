using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;



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
            // Debug.DrawRay(lastRay.origin, lastRay.direction * 100);   // Draws a line from start to start + dir in world coordinates.
            GetComponent<NavMeshAgent>().destination = hit.point;

        }
    }
}
