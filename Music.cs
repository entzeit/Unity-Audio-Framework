using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Music : Fade
{
    public AudioClip[] audioClips;
    private AudioSource[] audioSources;
    public AudioMixerGroup mixer;
    public GameManager gameManager = null;
    public float fadeTime = 5f;

    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSources = new AudioSource[audioClips.Length];
        for (int i = 0; i < audioClips.Length; ++i) {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].loop = true;
            audioSources[i].outputAudioMixerGroup = mixer;
            audioSources[i].clip = audioClips[i];
            audioSources[i].volume = 0.0f;
            audioSources[i].Play();
        }
        FadeIn(audioSources[0], fadeTime);
    }

    // Update is called once per frame
    void Update()
    {
        LoadGameManager();
    }

    void LoadGameManager() {
        if (gameManager == null && SceneManager.GetActiveScene().buildIndex == 1) {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }

    public void Play(int audio) {
        if (audioSources[audio].volume != 0) return;
        //Debug.Log("Playing music clip " + audio);
        if (audio >= audioClips.Length) {
            Debug.Log("Audio Clip with this index does not exist");
            return;
        }
        for (int i = 0; i < audioSources.Length; ++i) {
            if (i == audio) continue;
            if (audioSources[i].volume != 0f) {
                FadeOut(audioSources[i], fadeTime);
            }
        }
        if (audioSources[audio].volume == 0) FadeIn(audioSources[audio], fadeTime);
    }
}
