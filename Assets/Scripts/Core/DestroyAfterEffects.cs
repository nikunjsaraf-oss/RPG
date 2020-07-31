using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffects : MonoBehaviour
    {
        private void Update() 
        {
            if(!GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(gameObject);
            }    
        }
    }
}