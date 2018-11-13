using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineWidthPulse : MonoBehaviour {

    public float pulseSpeed = 1;
    public float minWidth = 2f;
    public float maxWidth = 5;
    bool increasing = true;

    Outline outline;

	// Use this for initialization
	void Start () {
		outline = GetComponent<Outline>();
	}
	
	// Update is called once per frame
	void Update () {
		if (increasing)
        {
            if (outline.OutlineWidth > maxWidth)
            {
                increasing = false;
            }
            else
            {
                outline.OutlineWidth += (pulseSpeed*Time.deltaTime);
            }
        }
        else
        {
            if (outline.OutlineWidth < minWidth)
            {
                increasing = true;
            }
            else
            {
                outline.OutlineWidth -= (pulseSpeed * Time.deltaTime);
            }
        }
	}
}
