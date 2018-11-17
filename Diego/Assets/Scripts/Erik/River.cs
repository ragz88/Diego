using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour {

    public bool useLerp = false;
    public Transform destination;
    public float riverSpeed = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "EnergySource")
        {
            
            if (useLerp && Vector3.Distance(other.transform.position, destination.position) > 1.5f)
            {
                other.transform.position = Vector3.Lerp(other.transform.position, destination.position, riverSpeed * Time.deltaTime);
            }
            else
            {
                other.transform.position = Vector3.MoveTowards(other.transform.position, destination.position, riverSpeed * Time.deltaTime);
            }

            if (Vector3.Distance(other.transform.position, destination.position) <= 1.5f)
            {
                other.GetComponent<Animator>().enabled = true;
            }
            else
            {
                other.GetComponent<Animator>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "EnergySource")
        {
            other.GetComponent<Animator>().enabled = true;
        }
    }
}
