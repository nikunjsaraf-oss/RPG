using UnityEngine;
using RPG.Core;

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


        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {

            if (equippedWeapon != null)
            {
                Transform handTransform;
                handTransform = CheckHand(rightHand, leftHand);
                Instantiate(equippedWeapon, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        public bool HasProjectile() => projectile != null;

        public void LaunchProjectile(Transform left,Transform right, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, CheckHand(right, left).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
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