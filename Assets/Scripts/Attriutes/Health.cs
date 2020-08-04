using RPG.Stats;
using RPG.Saving;
using RPG.Core;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float RegenerateHealthPercentage = 70;
        [SerializeField] UnityEvent takeDamage;
        [SerializeField] UnityEvent onDie;
 
        float healthPoints = -1;
        bool isDead = false;


        private float GetInitaialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.health);
        }

        private void Start()  
        {
            GetComponent<BaseStats>().OnLevelUp += RegenerateHealth;
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.health);
            }
        }

        private void RegenerateHealth()
        {
            float RegenerateHealthPoints = (GetComponent<BaseStats>().GetStat(Stat.health) * RegenerateHealthPercentage) / 100;
            healthPoints = Mathf.Max(healthPoints, RegenerateHealthPoints);
        }

        public bool IsDead()
        {
            return isDead;
        }

      

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0)
            {
                onDie.Invoke();
                Die();
                AwardExperience(instigator);
            }
            else{
                takeDamage.Invoke();
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
            return healthPoints;
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return (healthPoints / GetComponent<BaseStats>().GetStat(Stat.health));
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
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if(healthPoints == 0)
            {
                Die();
            }
        }
    }
}