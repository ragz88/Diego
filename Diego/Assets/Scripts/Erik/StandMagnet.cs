using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandMagnet : MonoBehaviour {

    public Transform lockPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "EnergySource" && hit.GetComponent<LiftableObject>().beingCarried == false)
        {
            hit.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerStay(Collider hit)
    {
        if (hit.tag == "EnergySource" && hit.GetComponent<LiftableObject>().beingCarried == false)
        {
            hit.GetComponent<Rigidbody>().isKinematic = true;
            if (Vector3.Distance(hit.transform.position, lockPoint.position) > 0.01f || Quaternion.Angle(hit.transform.rotation, transform.rotation) > 0.1f)
            {
                hit.transform.position = Vector3.Lerp(hit.transform.position, lockPoint.position, 2.5f * Time.deltaTime);
                hit.transform.rotation = Quaternion.Slerp(hit.transform.rotation, transform.rotation, 2.5f * Time.deltaTime);
            }
            else
            {
                hit.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "EnergySource")
        {
            if (hit.GetComponent<LiftableObject>().beingCarried == false)
            {
                hit.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

}
