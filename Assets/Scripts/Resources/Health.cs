using RPG.Stats;
using RPG.Saving;
using RPG.Core;
using UnityEngine;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float RegenerateHealthPercentage = 70;
 
        float health = -1f;
        bool isDead = false;

        private void Start() 
        {
            GetComponent<BaseStats>().OnLevelUp += RegenerateHealth;

            if(health < 0)
            {
                health = GetComponent<BaseStats>().GetStat(Stat.health);
            }
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

        public float GetPercentage()
        {
           return (health / GetComponent<BaseStats>().GetStat(Stat.health)) * 100;
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