﻿using System;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
            [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpEffect = null;
        [SerializeField] bool shouldUseModifiers = false;

        public event Action OnLevelUp;

        int currentLevel = 1;
        Experience experience;

        private void Awake() 
        {
            Experience experience = GetComponent<Experience>();
           
        }
        private void Start()
        {
          currentLevel  = CalculateLevel();
          
        }

        


        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelledUpEffect();
                OnLevelUp();
            }
        }

        private void LevelledUpEffect()
        {
            Instantiate(levelUpEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return GetStats(stat) + GetAdditiveModifier(stat) * (1 + GetPercentageModifier(stat) / 100);
        }

        private float GetStats(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        private float GetPercentageModifier(Stat stat)
        { 
            if(!shouldUseModifiers) return 0;

            float total = 0;

                foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
                {
                    foreach(float modifier in provider.GetPercentageModifiers(stat))
                    {
                        total += modifier;
                    }
                }

                return total;
        }

        private float GetAdditiveModifier(Stat stat)
        {
            if(!shouldUseModifiers) return 0;
           
            float total = 0;

            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }

            return total;
        }

        public int GetLevel()
        {
            return currentLevel;
        }

        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if(experience == null) { return startingLevel; }


            float currentXP = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if(XPToLevelUp > currentXP)
                {
                    return level;
                }
            }
            return penultimateLevel + 1;

        }
    }
}
