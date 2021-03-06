using UnityEngine;
using System;
using UnityEngine.UI;
using RPG.Attributes;

namespace RPG.HUD
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }


        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.MaxHealth());
        }
    }
}