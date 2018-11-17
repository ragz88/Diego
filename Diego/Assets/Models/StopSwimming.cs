using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StopSwimming : MonoBehaviour {

    public GameObject Diego;
    public RuntimeAnimatorController BaseAnimation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        Diego.GetComponent<Animator>().runtimeAnimatorController = BaseAnimation;
    }
}
