using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicOperator : MonoBehaviour {

    public bool fadingOut = false;
    public AudioSource MusicPlayer;
    public float fadeInTime;
    public float fadeOutTime;
    public AudioClip[] Songs;
    int currentSong = 0;
    float volume;
	// Use this for initialization
	void Start () {
        MusicPlayer = GameObject.FindObjectOfType<MusicOperator>().GetComponent<AudioSource>();
        volume = MusicPlayer.volume;
        StartCoroutine(MusicOperator.FadeIn(MusicPlayer, fadeInTime,volume));
        //MusicPlayer.Play();
	}
	
	// Update is called once per frame
	void Update () {
        currentSong = SceneManager.GetActiveScene().buildIndex;
        if(MusicPlayer.isPlaying == false)
        {
            //StartCoroutine(MusicOperator.FadeOut(MusicPlayer, fadeOutTime));
            //MusicPlayer.Stop();
            MusicPlayer.clip = Songs[currentSong];
            StartCoroutine(MusicOperator.FadeIn(MusicPlayer, fadeInTime, volume));
            //MusicPlayer.Play();
        }

        if(fadingOut)
        {
            Debug.Log("Stop song");
            StartCoroutine(MusicOperator.FadeOut(MusicPlayer, fadeOutTime));

        }

    }

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
           

    }
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime, float Volume)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < Volume)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }

  
}
