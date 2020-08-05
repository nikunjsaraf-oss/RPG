using UnityEngine.AI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace RPG.SceneManagement

{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E, F
        }

        [SerializeField] int loadScene = -1;
        [SerializeField] Transform spawnLocation;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeOutTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;



        private void OnTriggerEnter(Collider other)
        {
           if(other.tag == "Player")
            {
                //StartCoroutine(Transition());
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

       private IEnumerator Transition()
        {
            Fader fader = FindObjectOfType<Fader>();

            DontDestroyOnLoad(gameObject);

            yield return fader.FadeOut(fadeOutTime);

            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
           // wrapper.Save();

            yield return SceneManager.LoadSceneAsync(loadScene);

          //  wrapper.Load();
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

           // wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);


            Destroy(gameObject);
        }


        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnLocation.position;
            player.transform.rotation = otherPortal.spawnLocation.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) { continue; }
                if(portal.destination != destination) { continue; }

                return portal;
            }

            return null;
        }
    }
}

