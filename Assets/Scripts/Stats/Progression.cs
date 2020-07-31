using UnityEngine;
using System.Collections.Generic;
using System;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject 
    {
        [SerializeField] CharacterStats[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;


        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookupTable();
            float[] levels = lookupTable[characterClass][stat];

            if(levels.Length < level)
            {
                return 0;
            }

            return levels[level - 1];
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookupTable();
            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookupTable()
        {
            if(lookupTable != null) { return; }
            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach(CharacterStats characterStats in characterClasses)
            {
                var stackLookup = new Dictionary<Stat, float[]>();

                foreach (ProgressionStats progressionStats in characterStats.stats)
                {
                    stackLookup[progressionStats.stat] = progressionStats.levels;
                }

                lookupTable[characterStats.characterClass] = stackLookup;
            }

        }

        [System.Serializable]
        class CharacterStats
        {
            public CharacterClass characterClass;
            public ProgressionStats[] stats;
        }

        [System.Serializable]
        class ProgressionStats
        {
            public Stat stat;
            public float[] levels;
        }
    }
}