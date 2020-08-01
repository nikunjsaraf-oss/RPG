using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Controller;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void OnEnable() 
        {
            GetComponent<PlayableDirector>().stopped += EnableControl;
            GetComponent<PlayableDirector>().played += DisableControl;
        }

          private void OnDisable() 
        {
            GetComponent<PlayableDirector>().stopped -= EnableControl;
            GetComponent<PlayableDirector>().played -= DisableControl;
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