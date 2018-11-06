using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCapeColliders : MonoBehaviour {

    public Collider[] cols;
    bool activated = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!activated)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i].enabled = true;
            }
            activated = true;
        }
	}
}
