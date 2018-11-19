using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbHover : MonoBehaviour {

    //public Transform bottom;
    public float hoverSpeed;
    public float hoverAmplitude;
    public float lerpSpeed;
    public Vector3 recentre; 

    GameObject cube;

	// Use this for initialization
	void Start () {
        cube = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Vector3.down);
        transform.position = Vector3.Lerp(transform.position, cube.transform.position + recentre + 
            (hoverAmplitude * Vector3.up * Mathf.Sin(Time.time*hoverSpeed)), lerpSpeed * Time.deltaTime);
	}
}
