using UnityEngine;
using RPG.Attributes;
using RPG.Controller;
 
namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class EnemyTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HandleRayCast(PlayerController callingController)
        {
            if (!callingController.GetComponent<Fight>().CanAttack(gameObject))
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                callingController.GetComponent<Fight>().Attack(gameObject);
            }
            return true;
        }
    }

}