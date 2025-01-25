using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    IEnumerator Fading(AudioSource audioSource, float duration, float targetVolume, bool stop) {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration) {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        if (stop) audioSource.Stop();
        yield break;
    }

    public void FadeIn(AudioSource audioSource, float duration) {
        StartCoroutine(Fading(audioSource, duration, 1, false));
    }

    public void FadeOut(AudioSource audioSource, float duration, bool stop=false) {
        StartCoroutine(Fading(audioSource, duration, 0, stop));
    }
}
