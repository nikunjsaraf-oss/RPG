using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject 
    {
        [SerializeField] CharacterStats[] characterClass = null;
    

        [System.Serializable]
        class CharacterStats
        {
            [SerializeField] CharacterClass characterClass;
            [SerializeField] float[] health = null;
        }
    }
}