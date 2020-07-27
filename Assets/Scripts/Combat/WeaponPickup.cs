using UnityEngine;
using System.Collections;
using System;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5f;

        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.tag == "Player")
            {
                other.GetComponent<Fight>().EquipWeapon(weapon);
                StartCoroutine(HidePickup(respawnTime));
            }
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
    }
 
}