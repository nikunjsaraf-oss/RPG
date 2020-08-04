using UnityEngine;
using System.Collections;
using System;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine coroutine = null;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }


        public IEnumerator FadeOut(float time)
        {
           return Fade(1, time);
        }

        public IEnumerator FadeIn(float time)
        {
            return Fade(0,time);
        }

         private IEnumerator Fade(float target, float time)
        {
            if(coroutine != null)
           {
               StopCoroutine(coroutine);
           }
           coroutine = StartCoroutine(FadeRoutine(target, time));
           yield return coroutine;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
             while (Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time); 
                yield return null;
            }
        }
    }
}
