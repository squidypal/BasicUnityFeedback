// Created by Evelyn Masters on 2026-1-9

using System.Collections;
using UnityEngine;

namespace Feedback
{
    public class CameraShaker : MonoBehaviour
    {
        public static CameraShaker Instance { get; private set; }
    
        private Vector3 initialPosition;

        void Awake()
        {
            initialPosition = transform.localPosition;
            if (Instance == null)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }

        // What to call from your script
        public void TriggerShake(float duration, float magnitude, float fadeInTime = 0f, float fadeOutTime = 0f)
        {
            StopAllCoroutines();
            StartCoroutine(PerformShake(duration, magnitude, fadeInTime, fadeOutTime));
        }

        IEnumerator PerformShake(float duration, float magnitude, float fadeInTime, float fadeOutTime)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float currentMagnitude = magnitude;

                // New system with fade in and fade out
                
                if (elapsed < fadeInTime && fadeInTime > 0)
                {
                    currentMagnitude = Mathf.Lerp(0, magnitude, elapsed / fadeInTime);
                }
                else if (elapsed > (duration - fadeOutTime) && fadeOutTime > 0)
                {
                    float remainingTime = duration - elapsed;
                    currentMagnitude = Mathf.Lerp(0, magnitude, remainingTime / fadeOutTime);
                }

                float x = Random.Range(-1f, 1f) * currentMagnitude;
                float y = Random.Range(-1f, 1f) * currentMagnitude;

                transform.localPosition = initialPosition + new Vector3(x, y, 0f);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = initialPosition;
        }
    }
}