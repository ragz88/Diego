using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    bool pressedKey = false;
    public CamLerp camlerp;
    public Button start;
    MusicOperator MusicPlayer;
    // Use this for initialization
    void Start () {
        MusicPlayer = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicOperator>();
    }
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) || Input.GetButton("Submit")) && (!pressedKey))
        {
            camlerp.switchPos(1);
            start.Select();
        }
	}

    public void QuitGameFin()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif

    }

    public void StartGame()
    {
        MusicPlayer.fadingOut = true;
        SceneManager.LoadScene(1);
    }

    public void credits()
    {
        MusicPlayer.fadingOut = true;
        SceneManager.LoadScene(5);
    }
}
