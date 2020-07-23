using RPG.Core;
using RPG.Movement;
using UnityEngine;
namespace RPG.Combat
{
    public class Fight : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float timeBetweenAttack = 1f;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

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

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttack)
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
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }


        public bool CanAttack(GameObject enemyTarget)
        {
            if(enemyTarget  == null) { return false; };
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
        }

        private void TriggerStopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }

        public void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }
    }
}

