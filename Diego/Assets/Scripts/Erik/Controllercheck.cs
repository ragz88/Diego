using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controllercheck : MonoBehaviour
{

    private int PS4_Controller = 0;
    public bool controllerConected = false;
    public Sprite E;
    public Sprite Square;
    public ObjectInteractions diegoInteractions;
    public bool mainMenu = false;
    public SpriteRenderer pressToStart;
    public Sprite pS4;
    public Sprite pC;
    void Update()
    {
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
            controllerConected = true;
            diegoInteractions.promptPic.sprite = Square;
            if (mainMenu)
            {
                pressToStart.sprite = pS4;
            }
            //Erik does controller things here
        }
        else
        {
            controllerConected = false;
            diegoInteractions.promptPic.sprite = E;
            if (mainMenu)
            {
                pressToStart.sprite = pC;
            }
            //There is no controller
        }

        PS4_Controller = 0;
    }
    
}