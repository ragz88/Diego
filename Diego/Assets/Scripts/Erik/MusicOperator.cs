using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicOperator : MonoBehaviour {

    public AudioClip[] Songs;
    public AudioSource MusicPlayer;
    int currentSong = 0;

	// Use this for initialization
	void Start () {

        MusicPlayer.Play();
	}
	
	// Update is called once per frame
	void Update () {
        currentSong = SceneManager.GetActiveScene().buildIndex;
        if(MusicPlayer.clip != Songs[currentSong])
        {
            MusicPlayer.Stop();
            MusicPlayer.clip = Songs[currentSong];
            MusicPlayer.Play();
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
}
