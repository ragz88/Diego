using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandMagnet : MonoBehaviour {

    public Transform lockPoint;

	// Use this for initialization
	void Start () {
		if (lockPoint == null)
        {
            lockPoint = transform;
        }
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
            Vector3 cubeLateralPos = new Vector3(hit.transform.position.x, 0, hit.transform.position.z);
            Vector3 lockLateralPos = new Vector3(lockPoint.position.x, 0, lockPoint.position.z);
            if (Vector3.Distance(cubeLateralPos, lockLateralPos) > 0.03f || Quaternion.Angle(hit.transform.rotation, transform.rotation) > 0.3f)
            {
                cubeLateralPos = Vector3.Lerp(cubeLateralPos, lockLateralPos, 1f * Time.deltaTime);
                //hit.transform.position = Vector3.Lerp(hit.transform.position, new Vector3(cubeLateralPos.x, hit.transform.position.y, cubeLateralPos.z), 1f * Time.deltaTime);
                hit.transform.position = new Vector3(cubeLateralPos.x, hit.transform.position.y, cubeLateralPos.z);
                hit.transform.rotation = Quaternion.Slerp(hit.transform.rotation, transform.rotation, 1f * Time.deltaTime);
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
