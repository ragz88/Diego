﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {

    bool loadingNext = false;
    public Image whiteOut;
    public float fadeDelay;
    public float fadeSpeed;
    bool fadingOut = false;
    bool fadingIn = true;
    public bool loadNextScene = true;
    public int SceneToLoad;

// Use this for initialization
void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (fadingOut)
        {
            whiteOut.color = new Color(whiteOut.color.r, whiteOut.color.g, whiteOut.color.b, whiteOut.color.a + (Time.deltaTime * fadeSpeed));
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
            whiteOut.color = new Color(whiteOut.color.r, whiteOut.color.g, whiteOut.color.b, whiteOut.color.a - (Time.deltaTime * fadeSpeed));
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
            loadingNext = true;
            Invoke("StartFade", fadeDelay);
        }
    }

    void StartFade()
    {
        fadingOut = true;
    }
}
