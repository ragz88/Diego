using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFades : MonoBehaviour {
    AudioSource SoundPlayer;
    public float volume = .5f;
    public float fadeInTime = 5f;
    public float fadeOutTime = 5f;
    public static bool PlayAmbientNoise = true;
    MusicOperator MusicPlayer;
    // Use this for initialization
    void Start () {
        SoundPlayer = gameObject.GetComponent<AudioSource>();
      
        MusicPlayer = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicOperator>();
        SoundPlayer.volume = 0;
        SoundPlayer.Play();
        StartCoroutine(SoundFades.FadeIn(SoundPlayer, fadeInTime, volume));
       
       
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Music is "+MusicPlayer.fadingOut);
        Debug.Log("Volume is " + volume);
        if (MusicPlayer.fadingOut == true)
        {
            StartCoroutine(SoundFades.FadeOut(SoundPlayer, fadeOutTime));
        }
        //if (MusicPlayer.fadingOut == false)
        //{
        //    StartCoroutine(SoundFades.FadeIn(SoundPlayer, fadeInTime, volume));
        //}
    }
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        //audioSource.Stop();
    }
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime, float Volume)
    {
        //audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < Volume)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}
