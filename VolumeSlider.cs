using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;
    public String ExposedParam;

    public void setVolume()
    {
        mixer.SetFloat(ExposedParam, Mathf.Log10(volumeSlider.value) * 20);
    }
}