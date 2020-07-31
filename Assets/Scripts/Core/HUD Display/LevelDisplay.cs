using System;
using RPG.Stats;
using UnityEngine.UI;
using UnityEngine;

namespace RPG.HUD
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats stats;
        private void Awake()
        {
           stats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }


        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0}", stats.CalculateLevel());
        }
    }
}
