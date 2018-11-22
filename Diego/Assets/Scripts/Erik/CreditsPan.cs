using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreditsPan : MonoBehaviour {

    public float delay;
    float timer = 0;
    bool fadeIn = false;

    bool fadeOut = false;
    bool setFadeIn = false;

    public float panSpeed = 1;
    public float fadeSpeed = 1;
    float currentDist = 0;
    public float maxDist = 15;
    SpriteRenderer rend;


    // Use this for initialization
    void Start () {
        rend = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > delay)
        {
            if (setFadeIn == false)
            {
                fadeIn = true;
            }
            setFadeIn = true;
        }
        else
        {
            timer += Time.deltaTime;
        }

        if (fadeIn)
        {
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, rend.color.a + (Time.deltaTime * fadeSpeed));
            if (rend.color.a >= 1f)
            {
                fadeIn = false;
            }
        }
        else if (timer > delay)
        {
            if (fadeOut)
            {
                rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, rend.color.a - (Time.deltaTime * fadeSpeed * 0.5f));
            }
            else
            {
                if (currentDist < maxDist)
                {
                    currentDist += panSpeed * Time.deltaTime;
                    gameObject.transform.Translate(0, panSpeed * Time.deltaTime, 0);
                }
                else
                {
                    fadeOut = true;
                }
            }
        }
        

	}
}
