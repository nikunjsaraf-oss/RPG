using RPG.Combat;
using RPG.Resources;
using RPG.Movement;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace RPG.Controller
{
    
    public class PlayerController : MonoBehaviour
    {

        enum CursorType
        {
            None,
            Movement,
            Combat,
            UI,
        }

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;

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

          //  if (InteractWithComponent()) return;
            if (CombatInteraction()) return;
            if (MovementInteraction()) return;
            SetCursorType(CursorType.None);
        }

        // private bool InteractWithComponent()
        // {
           
        //     RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());  // Casts a ray through the Scene and returns all hits.
        //     foreach(RaycastHit hit in hits)
        //     {
        //         IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
        //         foreach(IRaycastable raycastable in raycastables)
        //         {
        //            if(raycastable.HandleRayCast(this))
        //            {
        //                 SetCursorType(CursorType.Combat);
        //                 return true;
        //            }
        //         }
        //     }
        //     return false;
        // }

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

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fight>().Attack(target.gameObject);
                }
                SetCursorType(CursorType.Combat);
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
                    GetComponent<Mover>().StartMoving(hit.point, 1f);
                }
                SetCursorType(CursorType.Movement);
                return true;
            }
                return false;
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