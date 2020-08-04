using UnityEngine;
using System;
using RPG.Attributes;
using UnityEngine.UI;
using RPG.Combat;

namespace RPG.HUD
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
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.MaxHealth());
        }
    }
}