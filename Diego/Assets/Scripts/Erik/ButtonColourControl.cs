using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonColourControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    Color initColour, finColour;
    MusicOperator musicPlayer;
    Text text;

    // Use this for initialization
    void Start () {
        initColour = gameObject.GetComponentInChildren<Text>().color;
        initColour = new Color(initColour.r, initColour.g, initColour.b, initColour.a - .2f);
        finColour = new Color(initColour.r, initColour.g, initColour.b, 1);
        finColour = Color.white;
        text = gameObject.GetComponentInChildren<Text>();
        musicPlayer = GameObject.FindObjectOfType<MusicOperator>();
        text.font = musicPlayer.menuFont;

    }
	
	// Update is called once per frame
	void Update ()
    {
   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<Text>().color = finColour;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<Text>().color = initColour;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<Text>().color = Color.white;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<Text>().color = finColour;
    }
}
