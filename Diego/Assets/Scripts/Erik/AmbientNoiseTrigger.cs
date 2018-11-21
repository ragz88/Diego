using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientNoiseTrigger : MonoBehaviour {

    public bool turnOff = true;
    public bool turnON = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(turnOff == true)
        {
            turnON = false;
         
        }
        if (turnON == true)
        {
            turnOff = false;
         
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (turnOff == true)
        {
            
            SoundFades.PlayAmbientNoise = false;
        }
        if (turnON == true)
        {
          
            SoundFades.PlayAmbientNoise = true;
        }
    }
}

