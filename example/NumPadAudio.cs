using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumPadAudio : MonoBehaviour
{
    public AudioClip correct;
    public AudioClip wrong;
    public AudioSource padAudioSource;

    public AudioClip[] clickAudioClips;
    public AudioSource clickAudioSource;
    private int lastClickClipSelect;

    public AudioClip[] numberAudioClips;
    public AudioSource numberAudioSource;
    private int[] digits;
    private bool sayNumber;
    private float delayBetweenDigits = 2f;
    private float delay = 8f;
    private float timer;
    private int counter;
    private bool firstSaying = true;

    private void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        SayNumber();
    }

    private void SayNumber() {
        if (!sayNumber) return;
        SayDigit();
    }

    private void SayDigit() {
        if (counter == 0) {
            if (firstSaying) {
                PlayDigit();
                counter++;
            } else {
                if (Time.time > timer + delay) {
                    PlayDigit();
                    counter++;
                }
            }
        } else if (counter == digits.Length - 1) {
            if (Time.time > timer + delayBetweenDigits) {
                PlayDigit();
                firstSaying = false;
                counter = 0;
            }
        } else {
            if (Time.time > timer + delayBetweenDigits) {
                PlayDigit();
                counter++;
            }
        }
    }

    private void PlayDigit() {
        numberAudioSource.clip = numberAudioClips[digits[counter]];
        numberAudioSource.Play();
        timer = Time.time;
    }


    public void StartSayingNumber(string number) {
        this.digits = new int[number.Length];
        for (int i = 0; i < number.Length; ++i) {
            digits[i] = (int)System.Char.GetNumericValue(number[i]);
            Debug.Log("Digit: " + digits[i]);
        }
        sayNumber = true;
    }

    public void StopSayingNumber() {
        sayNumber = false;
        firstSaying = true;
        counter = 0;
    }

    public void Click() {
        RoundRobinSelect();
        clickAudioSource.Play();
    }

    public void CorrectPad() {
        padAudioSource.clip = correct;
        padAudioSource.Play();
    }

    public void WrongPad() {
        padAudioSource.clip = wrong;
        padAudioSource.Play();
    }

    public void RoundRobinSelect() {
        if (clickAudioClips.Length == 0) {
            return;
        }
        int select;
        do {
            select = Random.Range(0, clickAudioClips.Length);
        } while (select == lastClickClipSelect);
        clickAudioSource.clip = clickAudioClips[select];
    }
}
