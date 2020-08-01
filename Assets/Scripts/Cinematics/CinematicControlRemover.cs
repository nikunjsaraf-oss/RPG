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
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            GetComponent<PlayableDirector>().stopped += EnableControl;
            GetComponent<PlayableDirector>().played += DisableControl;
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