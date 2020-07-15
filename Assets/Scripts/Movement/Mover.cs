using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    void Update()
    {  
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        UnityEngine.Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        UnityEngine.Vector3 localVelocity = transform.InverseTransformDirection(velocity);      // transform.InverseTransformDirection converts global to variable.
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
    }

    public void MoveTo(UnityEngine.Vector3 destination)
    {
        GetComponent<NavMeshAgent>().destination = destination;
    }
}
