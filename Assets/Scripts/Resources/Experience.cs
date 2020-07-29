using UnityEngine;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float ExpeirencePoints = 0;
        
        public void GainExperience(float experience)
        {
            ExpeirencePoints += experience; 
        }
    }
}