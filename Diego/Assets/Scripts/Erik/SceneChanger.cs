using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {

    bool loadingNext = false;
    public Image whiteOut;
    public float fadeInDelay = 1.5f;
    public float fadeOutDelay = 1.5f;
    public float fadeInSpeed =1;
    public float fadeOutSpeed =1;
    bool fadingOut = false;
    bool fadingIn = true;
    public bool loadNextScene = true;
    public int SceneToLoad;
    MusicOperator MusicPlayer;
    float startTimer = 0;
    bool timing = true;

// Use this for initialization
void Start ()
    {
        MusicPlayer = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicOperator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timing)
        {
            if (startTimer < fadeInDelay)
            {
                startTimer += Time.deltaTime;
            }
            else
            {
                fadingIn = true;
                timing = false;
            }
        }


        if (MusicPlayer == null)
        {
            Debug.Log("Music Player was Null");
            //MusicPlayer = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicOperator>();

        }

        if (fadingOut)
        {
            whiteOut.color = new Color(whiteOut.color.r, whiteOut.color.g, whiteOut.color.b, whiteOut.color.a + (Time.deltaTime * fadeOutSpeed));
            if (whiteOut.color.a >= 1)
            {
                if (loadNextScene)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene(SceneToLoad);
                }
                
            }
        }
        else if (fadingIn)
        {
            whiteOut.color = new Color(whiteOut.color.r, whiteOut.color.g, whiteOut.color.b, whiteOut.color.a - (Time.deltaTime * fadeInSpeed));
            if (whiteOut.color.a <= 0)
            {
                fadingIn = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!loadingNext && other.gameObject.tag == "Player")
        {
            MusicPlayer.fadingOut = true;
            loadingNext = true;
            Invoke("StartFade", fadeOutDelay);
        }
    }

    void StartFade()
    {
        fadingOut = true;
    }
}
