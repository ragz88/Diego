using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideyBox : MonoBehaviour {

    public TPCEngine.TPCharacter charac;
    TPCEngine.TPCMotor charMotor;


	// Use this for initialization
	void Start () {
        charMotor = charac.characterMotor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" )
        {
            if (charMotor.getIsCrouching())
            {
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            }
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("IgnoreRaycast");
        }
    }
}
