using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject 
    {
        [SerializeField] CharacterStats[] characterClasses = null;
    


        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (CharacterStats characterStats in characterClasses)
            {
                if(characterStats.characterClass == characterClass)
                {
                    return characterStats.health[level - 1];
                }
            }
            return 0;
        }

        [System.Serializable]
        class CharacterStats
        {
            public CharacterClass characterClass;
            public float[] health = null;
        }
    }
}