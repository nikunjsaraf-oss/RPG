using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Controller;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Start()
        { 
            GetComponent<PlayableDirector>().stopped += EnableControl;
            GetComponent<PlayableDirector>().played += DisableControl;
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void DisableControl(PlayableDirector pD)
        {
            player.GetComponent<ActionScheduler>().CancelAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pD)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }

} 