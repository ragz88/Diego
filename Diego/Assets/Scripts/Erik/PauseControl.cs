﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseControl : MonoBehaviour {

    public TPCEngine.TPCamera tpCam;
    Image PanelIm;
    public GameObject Panel;
    public Button initButton;
    

    public bool paused = false;
    float initZoom;

	// Use this for initialization
	void Start () {
        PanelIm = Panel.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
        {
            /*if (paused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }*/
            if (!paused)
            {
                initZoom = tpCam.DefaultDistance;
            }
            paused = !paused;
            initButton.Select();
        }

        if (paused && Input.GetButtonDown("Cancel"))
        {
            UnPause();
        }

        if (paused)
        {
            Panel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            if (PanelIm.color.a < 0.4f)
            {
                PanelIm.color = new Color(PanelIm.color.r, PanelIm.color.g, PanelIm.color.b, PanelIm.color.a + 0.02f);
            }
            //if (tpCam.DefaultDistance < tpCam.MaxDistance)
            ///{
            //    tpCam.DefaultDistance = tpCam.DefaultDistance + 0.02f;
            //}
            Time.timeScale = 0;
        }
        else
        { 
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (PanelIm.color.a > 0f)
            {
                PanelIm.color = new Color(PanelIm.color.r, PanelIm.color.g, PanelIm.color.b, PanelIm.color.a - 0.02f);
            }
            else
            {
                Panel.SetActive(false);
            }
            
        }
	}

    public void UnPause()
    {
        paused = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    public void QuitGameFin()
    {
        
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        
    }

    public void QuitGame()
    {
        //if (paused)
        //{
        paused = false;
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
        //}
    }

    public void RestartLvl()
    {
        //if (paused)
        //{
        paused = false;
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
    }
}
