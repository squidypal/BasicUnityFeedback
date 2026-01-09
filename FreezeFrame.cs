// Created by Evelyn Masters on 2026-1-9

using System.Collections;
using UnityEngine;

namespace Feedback
{
    public class FreezeFrame : MonoBehaviour
    {
        public static FreezeFrame Instance { get; private set; }
        // Freeze sound must be IN the audio source if you choose to use it
        private AudioSource audioSource;

        private bool isFrozen = false;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            if(GetComponent<AudioSource>())
                audioSource = GetComponent<AudioSource>();
        }

        // This is what is called by other scripts
        public void StartFreeze(float duration = 0.05f, float unfreezeTime = 0.1f)
        {
            if (isFrozen)
            {
                return;
            }
            
            if(!audioSource)
                audioSource.Play();
            
            StartCoroutine(Freeze(duration, unfreezeTime));
        }
    

        private IEnumerator Freeze(float freezeDuration, float unfreezeDuration)
        {
            isFrozen = true;

            // Just simply set timeScale to 0 for the duration of the freeze
            Time.timeScale = 0.0f;

            yield return new WaitForSecondsRealtime(freezeDuration);

            // This is so you can have a smoother transition back to full time, rather than instantly going back
            float timer = 0f;
            while (timer < unfreezeDuration)
            {
                Time.timeScale = Mathf.Lerp(0.0f, 1.0f, timer / unfreezeDuration);
                timer += Time.unscaledDeltaTime;
                yield return null;
            }

            Time.timeScale = 1.0f;
            isFrozen = false;
        }
    }
}