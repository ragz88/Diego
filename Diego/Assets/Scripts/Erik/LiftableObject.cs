﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftableObject : MonoBehaviour {

    public Vector3 initialPos;
    public bool useStartingPos = true;
    //public Transform partAttractorTrans;
    public bool beingCarried = false;
    float lightDelay = 0;
    public GameObject lightProjector;
    public bool inRange = false;
    public ObjectInteractions Interactions;
    public float dissolveTime = 1;
    public GameObject dissolveEffects;
    public GameObject partAttractor;
    //public ErikParticleAttractorLinear partAttractor;
    Outline outline;

    Animator anim;

    AudioSource dissolveSound;

	// Use this for initialization
	void Start () {
        partAttractor.transform.parent = null;
        if (useStartingPos)
        {
            initialPos = transform.position;
        }
        anim = gameObject.GetComponent<Animator>();
        dissolveSound = gameObject.GetComponent<AudioSource>();
        //dissolveTime = 2 * (anim.);
        outline = GetComponent<Outline>();
    }
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetKeyDown(KeyCode.K))
        {
            Dissolve();
        }*/
        if (beingCarried || !inRange)
        {
            outline.enabled = false;
        }
        inRange = false;

        if (beingCarried)
        {
            if (lightDelay < 0.8f && !lightProjector.activeSelf)
            {
                lightDelay += Time.deltaTime;
            }
            else
            {
                lightProjector.SetActive(true);
            }
        }
        else
        {
            if (lightDelay > 0f && lightProjector.activeSelf)
            {
                lightDelay -= 3*Time.deltaTime;
            }
            else
            {
                lightProjector.SetActive(false);
            }
        }
	}

    public void Dissolve()
    {
        anim.SetBool("Dissolve", true);
        Invoke("Undissolve", 0.1f);
        Invoke("ReturnToInit", dissolveTime/2);
        //GameObject tempEffects = Instantiate(dissolveEffects, transform.position, Quaternion.identity) as GameObject;
        dissolveEffects.GetComponent<ParticleSystem>().Play();
        //tempEffects.GetComponentInChildren<particleAttractorLinear>().target = partAttractorTrans;
        //tempEffects.transform.LookAt(initialPos);
        dissolveSound.Play();
    }

    void Undissolve()
    {
        //anim.SetBool("Undissolve", true);
        anim.SetBool("Dissolve", false);
    }

    public void ReturnToInit()
    {
        Invoke("resetBeingCarried", dissolveTime / 2);
        //dissolveEffects.GetComponent<ParticleSystem>().Pause();
        //anim.SetBool("Undissolve", false);
        //Interactions.dropObject();
        transform.position = initialPos;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void resetBeingCarried()
    {
        beingCarried = false;
    }

}
