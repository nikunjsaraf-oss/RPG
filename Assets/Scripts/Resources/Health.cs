using RPG.Stats;
using RPG.Saving;
using RPG.Core;
using UnityEngine;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float RegenerateHealthPercentage = 70;
 
        float health = -1;
        bool isDead = false;

        private float GetInitaialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.health);
        }

        private void Start()  
        {
           if (health < 0)
            {
                health = GetComponent<BaseStats>().GetStat(Stat.health);
            }
        }

        private void OnEnable()
        {
             GetComponent<BaseStats>().OnLevelUp += RegenerateHealth;
        }

        private void OnDisable() 
        {
             GetComponent<BaseStats>().OnLevelUp -= RegenerateHealth;    
        }

        private void RegenerateHealth()
        {
            float RegenerateHealthPoints = (GetComponent<BaseStats>().GetStat(Stat.health) * RegenerateHealthPercentage) / 100;
            health = Mathf.Max(health, RegenerateHealthPoints);
        }

        public bool IsDead()
        {
            return isDead;
        }

      

        public void TakeDamage(GameObject instigator, float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if(health == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward)); 
        }

        public float GetHealthPoints()
        {
            return health;
        }

        public float MaxHealth()
        {
           return GetComponent<BaseStats>().GetStat(Stat.health);
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