using RPG.Stats;
using RPG.Saving;
using RPG.Core;
using UnityEngine;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100f;

        bool isDead = false;

        private void Start() 
        {
            health = GetComponent<BaseStats>().GetHealth();    
        }

        public bool IsDead()
        {
            return isDead;
        }

       

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if(health == 0)
            {
                Die();
            }
        }

        public float GetPercentage()
        {
            return 100 * (health / GetComponent<BaseStats>().GetHealth());
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("dead");
            GetComponent<ActionScheduler>().CancelAction();
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;

            if(health == 0)
            {
                Die();
            }
        }
    }

}