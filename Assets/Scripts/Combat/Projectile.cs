﻿using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 1f;
        [SerializeField] GameObject hitFX = null;
        float damage = 0;
        Health target = null;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        // Update is called once per frame
        void Update()
        {
            if(target == null)
            {
                return;
            }

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
            if(target.IsDead()) return;
            target.TakeDamage(damage);
            Instantiate(hitFX, GetAimLocation(), transform.rotation);
            Destroy(gameObject);    
        }
    }
}