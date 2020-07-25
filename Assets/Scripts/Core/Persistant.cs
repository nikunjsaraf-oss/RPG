using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{

    public class Persistant : MonoBehaviour
    {
        [SerializeField] GameObject persistantObject;

        static bool hasSpawned = false;

        private void Awake()    // Get calls before any function!!!!!!!!!!!!!!
        {
            if (hasSpawned)
            {
                return;
            }

            SpawnPersistantObject();

            hasSpawned = true;
        }

        private void SpawnPersistantObject()
        {
            GameObject persistantObj = Instantiate(persistantObject);
            DontDestroyOnLoad(persistantObj);
        }
    }

}