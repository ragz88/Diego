using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftableObject : MonoBehaviour {

    public Vector3 initialPos;
    public bool useStartingPos = true;
    //public Transform partAttractorTrans;
    public bool beingCarried = false;
    public float lightIntensity = 0;
    public Light lightProjector;
    public bool inRange = false;
    public ObjectInteractions Interactions;
    public float dissolveTime = 1;
    public GameObject dissolveEffects;
    public GameObject partAttractor;
    //public ErikParticleAttractorLinear partAttractor;
    Outline outline;
    public Transform optionalStartingPos;
    public bool updateStartPos = false;
    //[HideInInspector]
    //public bool inDoor = false;

    Animator anim;

    AudioSource dissolveSound;

	// Use this for initialization
	void Start () {
        partAttractor.transform.parent = null;
        if (useStartingPos)
        {
            initialPos = transform.position;
        }
        else if (optionalStartingPos != null)
        {
            initialPos = optionalStartingPos.position;
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
        if (updateStartPos)
        {
            initialPos = optionalStartingPos.position;
        }

        if (beingCarried || !inRange)
        {
            outline.enabled = false;
        }
        inRange = false;

        if (beingCarried)
        {
            if (lightProjector.intensity < lightIntensity)
            {
                lightProjector.intensity += 2.5f*Time.deltaTime;
            }
        }
        else
        {
            if (lightProjector.intensity > 0)
            {
                lightProjector.intensity -= 3*Time.deltaTime;
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
