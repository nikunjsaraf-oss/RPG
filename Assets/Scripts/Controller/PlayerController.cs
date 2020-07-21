using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using UnityEngine;

namespace RPG.Controller
{
    
    public class PlayerController : MonoBehaviour
    {
        Health health;
        private void Start()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {
            if (health.IsDead())
            {
                return;
            }

            if (CombatInteraction()) return;
            if (MovementInteraction()) return;
        }

        private bool CombatInteraction()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());  // Casts a ray through the Scene and returns all hits.
            foreach(RaycastHit hit in hits)
            {
                EnemyTarget target = hit.transform.GetComponent<EnemyTarget>();

                if(target == null) { continue;  }

                if (!GetComponent<Fight>().CanAttack(target.gameObject))
                {
                    continue;   
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fight>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool MovementInteraction()
        {
           
            RaycastHit hit;         // Creates a ray starting at origin along direction.
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoving(hit.point);
                }
                return true;
            }
                return false;
        }

      
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
            // Returns a ray going from camera through a screen point.
        }
    }
}