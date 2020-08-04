using UnityEngine;
using System.Collections;
using RPG.Controller;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5f;
        [SerializeField] float restoreHealth = 0f;

        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.tag == "Player")
            {
                PiclUp(other.GetComponent<Fight>());
            }
        }

        private void PiclUp(Fight fighter)
        {
            fighter.EquipWeapon(weapon);
            StartCoroutine(HidePickup(respawnTime));
        }

        private IEnumerator HidePickup(float time)
        {
            ShowItem(false);
            yield return new WaitForSeconds(time);
            ShowItem(true);
        }

        private void ShowItem(bool shouldShow)
        {
            
            GetComponent<Collider>().enabled = shouldShow;
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        public bool HandleRayCast(PlayerController callingController)
        {
            if(Input.GetMouseButtonDown(0))
            {
                PiclUp(callingController.GetComponent<Fight>());
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.PickUp;
        }
    }
 
}