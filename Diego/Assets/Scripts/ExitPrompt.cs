using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitPrompt : MonoBehaviour {

    public Image exitPrompt;
    public Image fade;
    public Sprite pc;
    public Sprite ps4;
    private int PS4_Controller = 0;
    public float fadeSpeed;
    public float whiteSpeed;
    bool fadeout = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        exitPrompt.color = new Color(exitPrompt.color.r, exitPrompt.color.g, exitPrompt.color.b, exitPrompt.color.a + (Time.deltaTime * fadeSpeed));
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            //print(names[x].Length);
            if (names[x].Length == 19)
            {
                print("PS4 CONTROLLER IS CONNECTED");
                PS4_Controller = 1;

            }
        }
        if (PS4_Controller == 1)
        {
     
                exitPrompt.sprite = ps4;
           
        }
        else
        {
               exitPrompt.sprite = pc;
        }

        PS4_Controller = 0;
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("ControlerCancel")))
        {
           
            fadeout = true;

        }
        if(fadeout)
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a + (Time.deltaTime * whiteSpeed));
            if (fade.color.a >= 1)
            {
                TitleFade.FADENUM = 0;
                SceneManager.LoadScene(0);

            }
        }
    }
}
