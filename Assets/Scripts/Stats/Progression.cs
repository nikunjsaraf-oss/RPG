using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject 
    {
        [SerializeField] CharacterStats[] characterClasses = null;
    


        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            foreach (CharacterStats characterStats in characterClasses)
            {
                if (characterStats.characterClass != characterClass) continue;

                foreach(ProgressionStats progressionStats in characterStats.stats)
                {
                    if (progressionStats.stat != stat) continue;
                    if (progressionStats.levels.Length < level) continue;

                    return progressionStats.levels[level - 1];
                }
            }
            return 0;
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