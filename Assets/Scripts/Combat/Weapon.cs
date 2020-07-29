using UnityEngine;
using RPG.Core;
using RPG.Resources;
using System;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject equippedWeapon = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] float weaponRange = 0;
        [SerializeField] float weaponDamage = 0;
        [SerializeField] bool isRightHand = true;
        [SerializeField] Projectile projectile = null;


        const string weaponName = "weapon";

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if (equippedWeapon != null)
            {
                Transform handTransform;
                handTransform = CheckHand(rightHand, leftHand);
                GameObject weapon = Instantiate(equippedWeapon, handTransform);
                weapon.name = weaponName;
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else
            {
                var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
                if(overrideController != null)
                {
                    animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
                }       
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null){
                oldWeapon = leftHand.Find(weaponName);
            }
            if(oldWeapon == null){return; }

            oldWeapon.name = "DESTORYING";
            Destroy(oldWeapon.gameObject);
        }

        public bool HasProjectile() => projectile != null;

        public void LaunchProjectile(Transform left,Transform right, Health target, GameObject instigator)
        {
            Projectile projectileInstance = Instantiate(projectile, CheckHand(right, left).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, weaponDamage);
        }

        private Transform CheckHand(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHand)
            {
                handTransform = rightHand;
            }
            else
            {
                handTransform = leftHand;
            }

            return handTransform;
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }
    }
}