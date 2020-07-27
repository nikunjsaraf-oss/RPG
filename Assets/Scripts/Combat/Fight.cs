using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using UnityEngine;
namespace RPG.Combat
{
    public class Fight : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeBetweenAttack = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        Weapon currentWeapon = null;

        private void Start()
        {
            if(currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
        }



        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;

            if (target != null && !GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }


        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            currentWeapon.Spawn(rightHandTransform,leftHandTransform, animator);
        }


        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
        }


        public bool CanAttack(GameObject enemyTarget)
        {
            if (enemyTarget == null) { return false; };
            Health targetToTest = enemyTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }


        public void Attack(GameObject enemyTarget)
        {

            GetComponent<ActionScheduler>().StartAction(this);
            target = enemyTarget.GetComponent<Health>();

        }

        public void Cancel()
        {
            TriggerStopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void TriggerStopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }

        public void Hit()
        {
            if (target == null) return;
            if(currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetDamage());
            }
        }

        public void Shoot() => Hit();

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}

