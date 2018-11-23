using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleFade : MonoBehaviour {
    SpriteRenderer title;
    bool isfaded = true;
    public bool isLooping;
    public bool fadeOut;
    public bool fadeIn;
    public float fadeSpeed = 1;
    public static int FADENUM = 0;
    public int fadeNum;
    bool pressed = false;
    public GameObject thingToDestroy;
    MusicOperator MusicPlayer;
	// Use this for initialization
	void Start () {
        GameObject musicPlayerTemp = GameObject.FindGameObjectWithTag("Music");
        if (musicPlayerTemp != null)
        {
            MusicPlayer = musicPlayerTemp.GetComponent<MusicOperator>();
        }

        title = this.GetComponent<SpriteRenderer>();
        title.color = new Color(title.color.r, title.color.g, title.color.b, 0);
        if(fadeOut)
        {
            title.color = new Color(title.color.r, title.color.g, title.color.b, 1);
        }
        
    }
	
	// Update is called once per frame
	void Update () {


        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("ControlJump")) && (fadeNum == TitleFade.FADENUM))
        {
            pressed = true;

        }

            if (isLooping)
        {
            if(isfaded)
            {
               title.color = new Color(title.color.r, title.color.g, title.color.b, title.color.a + (Time.deltaTime * fadeSpeed));
               if (title.color.a >= 1)
               {
                    isfaded = false;

               }
            }
            if (!isfaded)
            {
                title.color = new Color(title.color.r, title.color.g, title.color.b, title.color.a - (Time.deltaTime * fadeSpeed));
                if (title.color.a <= 0)
                {
                    isfaded = true;

                }
            }

        }
        if (fadeOut)
        {
            if(fadeNum == TitleFade.FADENUM)
            {
              title.color = new Color(title.color.r, title.color.g, title.color.b, title.color.a - (Time.deltaTime * fadeSpeed));
              if (title.color.a <= 0)
              {
              
                  FADENUM++;
                  //title.enabled = false;
              }
            }

          

        }
        if (fadeIn && pressed)
        {
               thingToDestroy.SetActive(false);
               title.color = new Color(title.color.r, title.color.g, title.color.b, title.color.a + (Time.deltaTime * fadeSpeed));
               if (title.color.a >= 1)
               {
                   MusicPlayer.fadingOut = true;
                    SceneManager.LoadScene(1);

               }
            



            print(FADENUM);

        }
    }
}
