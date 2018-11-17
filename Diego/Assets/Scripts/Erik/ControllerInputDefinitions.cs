using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputDefinitions : MonoBehaviour {

    TPCEngine.TPCharacter character;
    TPCEngine.TPCMotor charMotor;

    // Use this for initialization
    void Start()
    {
        character = gameObject.GetComponent<TPCEngine.TPCharacter>();
        charMotor = character.characterMotor;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
