using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRobin : Fade
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private int lastClipSelect;

    public void RoundRobinSelect() {
        if (audioClips.Length == 0) {
            return;
        }
        int select;
        do {
            select = Random.Range(0, audioClips.Length);
        } while (select == lastClipSelect);
        audioSource.clip = audioClips[select];
        //Debug.Log("Sound played");
    }
}
