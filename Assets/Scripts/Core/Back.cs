using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    public void GoBack()
   {
       SceneManager.LoadScene(0);
   }
}
