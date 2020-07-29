using UnityEngine;
using System;
using RPG.Resources;
using UnityEngine.UI;


namespace RPG.Combat
{
    public class EnemyHealthValue : MonoBehaviour
    {
         Fight fighter;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fight>();
        }


        private void Update()
        {
            if(fighter.GetTarget() == null)
            {
                GetComponent<Text>().text = "N/A";
                return;
            }

            Health health = fighter.GetTarget();
            GetComponent<Text>().text = String.Format("{0}%", health.GetPercentage());
        }
    }
}