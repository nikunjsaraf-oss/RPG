using RPG.Combat;
using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
namespace RPG.Movement
{

    public class Mover : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            UnityEngine.Vector3 velocity = navMeshAgent.velocity;
            UnityEngine.Vector3 localVelocity = transform.InverseTransformDirection(velocity);      // transform.InverseTransformDirection converts global to variable.
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

        public void StartMoving(UnityEngine.Vector3 destination)
        {
            GetComponent<Fight>().Cancel();
            MoveTo(destination);
        }

        public void MoveTo(UnityEngine.Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }
    }

}