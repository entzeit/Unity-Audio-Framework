using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAudio : RoundRobin
{
    private bool playing;

    public void Play() {
        playing = true;
        if (!audioSource.isPlaying) {
            RoundRobinSelect();
            audioSource.Play();
        } else {
            audioSource.Stop();
            RoundRobinSelect();
            audioSource.Play();
        }
    }

    public void Stop(float fadeOutDuration) {
        if (audioSource.isPlaying && playing) {
            playing = false;
            FadeOut(audioSource, fadeOutDuration, true);
        }
    }
}
