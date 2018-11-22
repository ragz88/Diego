﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartActivator : MonoBehaviour {

    public AuraAPI.AuraLight[] volLights;
    public AuraAPI.AuraVolume[] volumes;


    // Use this for initialization
    void Start () {
		for (int i = 0; i < volLights.Length; i++)
        {
            volLights[i].enabled = true;
        }

        for (int i = 0; i < volumes.Length; i++)
        {
            volumes[i].enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
