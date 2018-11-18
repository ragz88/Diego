using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour {

    public bool useLerp = false;
    public Transform destination;
    public float riverSpeed = 2f;
    public RuntimeAnimatorController [] Animators;
    public Avatar Diego;
    public TPCEngine.TPCharacter diegoChar;
    TPCEngine.TPCMotor diegoMot;
    public GameObject[] bankColliders;
    bool notswimming = true;
	// Use this for initialization
	void Start () {
        diegoMot = diegoChar.characterMotor;
	}
	
	// Update is called once per frame
	void Update () {
        if(notswimming)
        {
         // GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().runtimeAnimatorController = Animators[0];
        }
        
	}
    private void OnCollisionExit(Collision collision)
    {
       // swimming = false;
    }
    private void OnTriggerStay(Collider other)
    {

       // notswimming = false;

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
                other.GetComponent<Animator>().runtimeAnimatorController = Animators[0];
                other.GetComponent<Animator>().avatar = Diego;
            }
            else
            {
                
                other.GetComponent<Animator>().runtimeAnimatorController = Animators[1];
                other.GetComponent<Animator>().avatar = Diego;
            }

            for (int i = 0; i < bankColliders.Length; i++)
            {
                bankColliders[i].SetActive(true);
            }

            diegoMot.swimming = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "EnergySource")
        {
            other.GetComponent<Animator>().enabled = true;
            for (int i = 0; i < bankColliders.Length; i++)
            {
                bankColliders[i].SetActive(false);
            }
            diegoMot.swimming = false;
        }
    }
}
