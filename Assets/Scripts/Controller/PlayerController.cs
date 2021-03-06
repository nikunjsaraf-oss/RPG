﻿using RPG.Attributes;
using RPG.Movement;
using UnityEngine;
using UnityEngine.AI;
using System;

namespace RPG.Controller
{
    
    public class PlayerController : MonoBehaviour
    {

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float rayCastRadius = 1f;
        

        Health health;
        
        private void Start()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {
           
            if (health.IsDead())
            {
                SetCursorType(CursorType.None);
                return;
            }

            if (InteractWithComponent()) return;
            if (MovementInteraction()) return;
            SetCursorType(CursorType.None);
        }

        private bool InteractWithComponent()
        {
           
            RaycastHit[] hits = RaycastSorted();
            foreach(RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach(IRaycastable raycastable in raycastables)
                {
                   if(raycastable.HandleRayCast(this))
                   {
                        SetCursorType(raycastable.GetCursorType());       
                        return true;
                   }
                }
            }
            return false;
        }

        private RaycastHit[] RaycastSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(),rayCastRadius);  // Casts a ray through the Scene and returns all hits.
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private bool MovementInteraction()
        {
           
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);

            if (hasHit)
            {
                if(!GetComponent<Mover>().CanMoveTo(target)) return false;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoving(target, 1f);
                }
                SetCursorType(CursorType.Movement);
                return true;
            }
                return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();
            RaycastHit hit;         // Creates a ray starting at origin along direction.
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if(!hasHit) return false;
            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if(!hasCastToNavMesh) return false;
            target = navMeshHit.position;

           
            return true;
        }

        
        private void SetCursorType(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);

        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if(mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
            // Returns a ray going from camera through a screen point.
        }
    }
}