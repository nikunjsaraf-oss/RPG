using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 1f;

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

        public void SetTarget(Health target) => this.target = target;

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }
    }

}