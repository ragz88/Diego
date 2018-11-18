﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptControl : MonoBehaviour {

    public enum PromptType
    {
        Keyboard,
        Controller
    }

    public PromptType promptType;

    public enum PromptToShow
    {
        Crouch,
        Jump
    }

    public PromptToShow promptToShow;

    public ObjectInteractions diego;
    public Image promptImage;
    ParticleSystem promptParts; 
    public ParticleSystem circPromptParts;
    public ParticleSystem rectPromptParts;

    public Sprite jumpPic;
    public Sprite altJumpPic;
    public Sprite crouchPic;
    public Sprite altCrouchPic;
    public Sprite interactPic;
    public Sprite altInteractPic;

    [HideInInspector] public Sprite currentJumpSprite;
    [HideInInspector] public Sprite currentCrouchSprite;
    [HideInInspector] public Sprite currentInteractSprite;

    TPCEngine.TPCMotor diegoMotor;

    bool changedToControl = false;
    bool changedToKeyboard = true;

    bool showPrompt = false;

    public Controllercheck controlCheck;

    // Use this for initialization
    void Start () {
        currentCrouchSprite = crouchPic;
        currentInteractSprite = interactPic;
        currentJumpSprite = jumpPic;
        promptParts = circPromptParts;
        diegoMotor = diego.gameObject.GetComponent<TPCEngine.TPCharacter>().characterMotor;
}
	
	// Update is called once per frame
	void Update () {

        if (controlCheck.controllerConected)
        {
            promptType = PromptType.Controller;
        }
        else
        {
            promptType = PromptType.Keyboard;
        }

        if (promptType == PromptType.Controller && !changedToControl)
        {
            currentCrouchSprite = altCrouchPic;
            currentInteractSprite = altInteractPic;
            currentJumpSprite = altJumpPic;
            changedToControl = true;
            changedToKeyboard = false;
        }
        else if (promptType == PromptType.Keyboard && !changedToKeyboard)
        {
            currentCrouchSprite = crouchPic;
            currentInteractSprite = interactPic;
            currentJumpSprite = jumpPic;
            changedToControl = false;
            changedToKeyboard = true;
        }


        if (diego.prompt.activeInHierarchy)
        {
            showPrompt = false;
        }

        if (showPrompt && promptImage.color.a < 1)
        {
            promptImage.color = promptImage.color = new Color(promptImage.color.r, promptImage.color.g, promptImage.color.b, promptImage.color.a + (Time.deltaTime * 2));
        }
        else if (!showPrompt && promptImage.color.a > 0)
        {
            promptImage.color = promptImage.color = new Color(promptImage.color.r, promptImage.color.g, promptImage.color.b, promptImage.color.a - (Time.deltaTime * 2));
        }
	}

    public void setJumpPromt()
    {
        promptImage.sprite = currentJumpSprite;

        if (promptType == PromptType.Keyboard)
        {
            if (promptParts != rectPromptParts)
            {
                if (promptParts.gameObject.activeInHierarchy)
                {
                    promptParts.gameObject.SetActive(false);
                    promptParts.Pause();
                    promptParts = rectPromptParts;
                    promptParts.gameObject.SetActive(true);
                    promptParts.Play();
                }
                else
                {
                    promptParts = rectPromptParts;
                }
            }
            promptImage.rectTransform.sizeDelta = new Vector2(0.43f, promptImage.rectTransform.sizeDelta.y);
        }
        else
        {
            if (promptParts != circPromptParts)
            {
                if (promptParts.gameObject.activeInHierarchy)
                {
                    promptParts.gameObject.SetActive(false);
                    promptParts.Pause();
                    promptParts = circPromptParts;
                    promptParts.gameObject.SetActive(true);
                    promptParts.Play();
                }
                else
                {
                    promptParts = circPromptParts;
                }
            }
            promptImage.rectTransform.sizeDelta = new Vector2(0.25f, promptImage.rectTransform.sizeDelta.y);
        }
    }

    public void setCrouchPromt()
    {
        promptImage.sprite = currentCrouchSprite;

        if (promptType == PromptType.Keyboard)
        {
            if (promptParts != rectPromptParts)
            {
                if (promptParts.gameObject.activeInHierarchy)
                {
                    promptParts.gameObject.SetActive(false);
                    promptParts.Pause();
                    promptParts = rectPromptParts;
                    promptParts.gameObject.SetActive(true);
                    promptParts.Play();
                }
                else
                {
                    promptParts = rectPromptParts;
                }
                
            }
            promptImage.rectTransform.sizeDelta = new Vector2(0.43f, promptImage.rectTransform.sizeDelta.y);
        }
        else
        {
            if (promptParts != circPromptParts)
            {
                if (promptParts.gameObject.activeInHierarchy)
                {
                    promptParts.gameObject.SetActive(false);
                    promptParts.Pause();
                    promptParts = circPromptParts;
                    promptParts.gameObject.SetActive(true);
                    promptParts.Play();
                }
                else
                {
                    promptParts = circPromptParts;
                }
            }
            promptImage.rectTransform.sizeDelta = new Vector2(0.25f, promptImage.rectTransform.sizeDelta.y);
        }
    }

    public void setInteractPromt()
    {
        promptImage.sprite = currentInteractSprite;

        
        if (promptParts != circPromptParts)
        {
            if (promptParts.gameObject.activeInHierarchy)
            {
                promptParts.gameObject.SetActive(false);
                promptParts.Pause();
                promptParts = circPromptParts;
                promptParts.gameObject.SetActive(true);
                promptParts.Play();
            }
            else
            {
                promptParts = circPromptParts;
            }
        }
        promptImage.rectTransform.sizeDelta = new Vector2(0.25f, promptImage.rectTransform.sizeDelta.y);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (promptToShow == PromptToShow.Crouch && promptImage.sprite != currentCrouchSprite)
            {
                if (!diegoMotor.getIsCrouching())
                {
                    setCrouchPromt();
                }   
            }
            else if (promptToShow == PromptToShow.Jump && promptImage.sprite != currentJumpSprite)
            {
                setJumpPromt();
            }

            if (showPrompt == false)
            {
                promptParts.gameObject.SetActive(true);
                promptParts.Play();
                showPrompt = true;
            }

            if (promptToShow == PromptToShow.Crouch && diegoMotor.IsCrouching)
            {
                promptParts.gameObject.SetActive(false);
                promptParts.Pause();
                showPrompt = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            promptParts.gameObject.SetActive(false);
            promptParts.Pause();
            showPrompt = false;
        }
    }
}
