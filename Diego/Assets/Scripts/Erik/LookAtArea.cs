using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LookAtArea : MonoBehaviour {

    public Transform target;
    //public string affectedTag = "Enemy";
    public GameObject affectedObject;
    public bool x = false;
    public bool y = true;
    public bool z = false;

    bool turning = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (turning)
        {
            Transform temp = Instantiate(target, affectedObject.transform.position, affectedObject.transform.rotation);
            temp.LookAt(target);
            float tempx = affectedObject.transform.eulerAngles.x;
            float tempy = affectedObject.transform.eulerAngles.y;
            float tempz = affectedObject.transform.eulerAngles.z;

            if (x)
            {
                tempx = temp.eulerAngles.x;
            }
            if (y)
            {
                tempy = temp.eulerAngles.y;
            }
            if (z)
            {
                tempz = temp.eulerAngles.z;
            }

            //affectedObject.GetComponent<NavMeshAgent>().enabled = false;

            affectedObject.transform.eulerAngles = Vector3.Lerp(affectedObject.transform.eulerAngles, new Vector3(tempx, tempy, tempz), 1 * Time.deltaTime);

            //affectedObject.GetComponent<NavMeshAgent>().enabled = true;
            Destroy(temp.gameObject);
        }

        //turning = false;
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == affectedObject )
        {
            turning = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == affectedObject)
        {
            turning = false;
        }
    }
}
