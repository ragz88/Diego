using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatorSlashActivator : MonoBehaviour {

    public enum Action
    {
        Activate,
        Deactivate
    }

    public Action action;
    public GameObject[] objs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (action == Action.Activate)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    objs[i].SetActive(true);
                }
                
            }
            else
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    objs[i].SetActive(false);
                }
            }
        }
    }
}
