using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalPushable : MonoBehaviour {

    Animator anim;

    [HideInInspector]
    public bool animPlayed = false;
    public bool inRange = false;
    public GameObject[] objectsToActivate;
    public ParticleSystem clueParts;
    Outline outline;
    AudioSource sound;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        outline = GetComponent<Outline>();
        sound = GetComponent<AudioSource>();
        //anim.StopPlayback();
	}
	
	// Update is called once per frame
	void Update () {
        if (!inRange)
        {
            outline.enabled = false;
        }

        inRange = false;
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            outline.enabled = true;
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Interact")) && !animPlayed)
            {
                
                PlayAnim();
            }
        }
        else if (other.gameObject.tag == "Pushable")
        {
            PlayAnim();
        }
    }

    public void PlayAnim()
    {
        if (!animPlayed)
        {
            animPlayed = true;
            sound.Play();
            anim.SetBool("playAnim", true);
            for (int i = 0; i < objectsToActivate.Length; i++)
            {
                objectsToActivate[i].SetActive(true);
            }
            if (clueParts != null)
            {
                clueParts.Stop();
            }
            outline.enabled = false;
            Destroy(this);
        }
    }
}
