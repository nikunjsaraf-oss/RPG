﻿using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 1f;
        float damage = 0;
        Health target = null;

        // Update is called once per frame
        void Update()
        {
            if(target == null)
            {
                return;
            }
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
         {
            this.damage = damage;
            this.target = target;
         }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.GetComponent<Health>() != target) return;

            target.TakeDamage(damage);
            Destroy(gameObject);    
        }
    }
}