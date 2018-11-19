using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityStandardAssets.Characters.ThirdPerson;

public class ObjectInteractions : MonoBehaviour {

    //public string[] pickupTags;
    public bool enemy = false;
    public GameObject prompt;
    public Image promptPic;
    public Transform liftPos;
    public Transform altLiftPos;
    Transform currentLiftPos;
    public Transform pushPos;
    public float liftSpeed = 0.1f;
    float currentLiftSpeed = 0.1f;

    bool nextToPickup = false;
    bool nextToPushable = false;
    bool nextToEnvPushable = false;
    public bool holdingPickup = false;
    public bool pushingObject = false;


    public bool lerpingPickup = false;
    //public bool droppingPickup = false;
    bool transitioning = false;  //true on the first frame when e is pressed

    public const float bufferDist = 0.75f;

    public GameObject objectToLift;
    public GameObject objectToPush;

    public GameObject optionalCam;

    public LayerMask rayMask;

    //public MoveBehaviour characterControl;                            //Urgent - Must Repair ////////////////////////////////////////////////////////////////////////////
    //ThirdPersonCharacter character;

    //Text promptText;

    PushableObject pushBoxController = null;
    Transform pushPoint;
    //bool lockMovement = false;

    TPCEngine.TPCharacter character;
    TPCEngine.TPCMotor charMotor;

    // Use this for initialization
    void Start () {
        //promptText = prompt.GetComponent<Text>();
        //characterControl = gameObject.GetComponent<MoveBehaviour>();                 //Urgent - Must Repair ////////////////////////////////////////////////////////////////////////////
        //character = gameObject.GetComponentInParent<ThirdPersonCharacter>();
        currentLiftPos = liftPos;
        if (!enemy)
        {
            character = gameObject.GetComponent<TPCEngine.TPCharacter>();
            charMotor = character.characterMotor;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (!enemy)
        {
            if (charMotor.getIsCrouching())
            {
                currentLiftPos = altLiftPos;
            }
            else
            {
                currentLiftPos = liftPos;
            }
        }

        //PushableObject pushScript;
        LiftableObject liftScript = null;
        EnvironmentalPushable envPushable = null;
        pushBoxController = null;

        /*if (characterControl != null && characterControl.lockMovement == true)             //Urgent - Must Repair ////////////////////////////////////////////////////////////////////////////
        {
            dropObject();
        }*/


        //Raycasting (4 directions) ----------------------------
        RaycastHit rayHit;
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward + new Vector3(0, -0.5f, 0), out rayHit, 1.2f, rayMask))
        {
            pushBoxController = rayHit.collider.gameObject.GetComponent<PushableObject>();
            liftScript = rayHit.collider.gameObject.GetComponent<LiftableObject>();
            envPushable = rayHit.collider.gameObject.GetComponent<EnvironmentalPushable>();
        }

        if ( (pushBoxController == null && liftScript == null && envPushable == null) &&
            Physics.Raycast(transform.position + new Vector3(0, 1, 0) + 0.25f * transform.right, transform.forward + new Vector3(0, -0.5f, 0) + 1.5f*transform.right, out rayHit, 1.2f, rayMask))
        {
            pushBoxController = rayHit.collider.gameObject.GetComponent<PushableObject>();
            liftScript = rayHit.collider.gameObject.GetComponent<LiftableObject>();
            envPushable = rayHit.collider.gameObject.GetComponent<EnvironmentalPushable>();
        }

        if ((pushBoxController == null && liftScript == null && envPushable == null) &&
            Physics.Raycast(transform.position + new Vector3(0, 1, 0) - 0.25f * transform.right, transform.forward + new Vector3(0, -0.5f, 0) - 1.5f*transform.right, out rayHit, 1.2f, rayMask))
        {
            pushBoxController = rayHit.collider.gameObject.GetComponent<PushableObject>();
            liftScript = rayHit.collider.gameObject.GetComponent<LiftableObject>();
            envPushable = rayHit.collider.gameObject.GetComponent<EnvironmentalPushable>();
        }

        if (optionalCam != null)
        {
            if ((pushBoxController == null && liftScript == null && envPushable == null) &&
                Physics.Raycast(optionalCam.transform.position, optionalCam.transform.forward, out rayHit, (Vector3.Distance(transform.position, optionalCam.transform.position) + 2f), rayMask))
            {
                pushBoxController = rayHit.collider.gameObject.GetComponent<PushableObject>();
                liftScript = rayHit.collider.gameObject.GetComponent<LiftableObject>();
                envPushable = rayHit.collider.gameObject.GetComponent<EnvironmentalPushable>();
            }
        }

        Debug.DrawRay(transform.position + new Vector3(0, 1, 0) + 0.25f * transform.right, (transform.forward + new Vector3(0, -0.5f, 0) + 1.5f * transform.right).normalized * 1.2f, Color.magenta);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0) - 0.25f * transform.right, (transform.forward + new Vector3(0, -0.5f, 0) - 1.5f * transform.right).normalized * 1.2f, Color.magenta);
        if (optionalCam != null)
        {
            Debug.DrawRay(optionalCam.transform.position, (optionalCam.transform.forward).normalized * (Vector3.Distance(transform.position, optionalCam.transform.position) + 1.2f), Color.green);
        }
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), (transform.forward + new Vector3(0, -0.5f, 0)).normalized * 1.2f, Color.magenta);


        //-------------------------------------------------------


        //Raycast resolution ----------------------
        if (liftScript != null /*&& characterControl.lockMovement != true*/)       //Urgent - Must Repair ////////////////////////////////////////////////////////////////////////////
        {
            objectToLift = liftScript.gameObject;
            nextToPickup = true;
        }
        else
        {
            if (!holdingPickup)
            {
                objectToLift = null;
            }
            nextToPickup = false;

            if (pushBoxController != null)
            {
                objectToPush = pushBoxController.gameObject;
                nextToPushable = true;
            }
            else
            {
                if (!pushingObject)
                {
                    objectToPush = null;
                }
                nextToPushable = false;

                if (envPushable != null)
                {
                    nextToEnvPushable = true;
                }
                else
                {
                    nextToEnvPushable = false;
                }
            }
        }
        //-----------------------------------------


        

        if (objectToLift != null)
        {
            if (nextToPickup && !holdingPickup && !pushingObject && !objectToLift.GetComponent<LiftableObject>().beingCarried)
            {
                if (!enemy)
                {
                    prompt.SetActive(true);
                    prompt.GetComponent<ParticleSystem>().Play();
                    if (promptPic.color.a < 1f)
                    {
                        promptPic.color = new Color(promptPic.color.r, promptPic.color.g, promptPic.color.b, promptPic.color.a + (Time.deltaTime * 2));
                    }
                }
                objectToLift.GetComponent<Outline>().enabled = true;
                objectToLift.GetComponent<LiftableObject>().inRange = true;
                if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Interact"))          //Urgent -- Must Repair//////////////////////////////////////////////////
                {
                    //lerpingPickup = true;
                    objectToLift.GetComponent<LiftableObject>().beingCarried = true;
                    LiftObject();
                }
            }
        }

        if (nextToPushable && !holdingPickup && !pushingObject)
        {
            if (!enemy)
            {
                prompt.SetActive(true);
                prompt.GetComponent<ParticleSystem>().Play();
                if (promptPic.color.a < 1f)
                {
                    promptPic.color = new Color(promptPic.color.r, promptPic.color.g, promptPic.color.b, promptPic.color.a + (Time.deltaTime * 2));
                }
            }
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Interact"))              //Urgent -- Must Repair//////////////////////////////////////////////////
            {
                transitioning = true;
                pushingObject = true;
                //characterControl.lockMovement = true;   //Urgent - Must Repair ////////////////////////////////////////////////////////////////////////////
                //characterControl.pushing = true;        //Urgent - Must Repair ////////////////////////////////////////////////////////////////////////////
                //character.pushingObject = true;
                objectToPush.GetComponent<Rigidbody>().mass = 200f;
                //gameObject.GetComponent<MoveBehaviour>().pushing = true;

                //objectToPush.GetComponent<Rigidbody>().isKinematic = true;
                //Physics.IgnoreCollision(objectToPush.GetComponent<Collider>(), gameObject.transform.parent.GetComponent<Collider>());
                //pushPos.position = objectToPush.transform.position;
                pushBoxController.beingPushed = true;
                float tempDist = Vector3.Distance(transform.position, pushBoxController.pushPoints[0].position);
                int nearestPoint = 0;
                for (int i = 0; i < pushBoxController.pushPoints.Length; i++)
                {
                    if (Vector3.Distance(transform.position, pushBoxController.pushPoints[i].position) < tempDist)
                    {
                        tempDist = Vector3.Distance(transform.position, pushBoxController.pushPoints[i].position);
                        nearestPoint = i;
                    }
                }
                pushPoint = pushBoxController.pushPoints[nearestPoint];
            }
        }


        if (nextToEnvPushable && !holdingPickup && !pushingObject)
        {
            if (envPushable.animPlayed == false)
            {
                if (!enemy)
                {
                    prompt.SetActive(true);
                    prompt.GetComponent<ParticleSystem>().Play();
                    if (promptPic.color.a < 1f)
                    {
                        promptPic.color = new Color(promptPic.color.r, promptPic.color.g, promptPic.color.b, promptPic.color.a + (Time.deltaTime * 2));
                    }
                }
                envPushable.GetComponent<Outline>().enabled = true;
                envPushable.inRange = true;
                if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Interact"))                                  //Urgent -- Must Repair//////////////////////////////////////////////////
                {
                    envPushable.PlayAnim();
                    prompt.SetActive(false);
                    prompt.GetComponent<ParticleSystem>().Pause();
                    if (promptPic.color.a > 0f)
                    {
                        promptPic.color = new Color(promptPic.color.r, promptPic.color.g, promptPic.color.b, promptPic.color.a - (Time.deltaTime * 2));
                    }
                }
            }

        }

        if ((!nextToPickup && !nextToPushable && !nextToEnvPushable) || holdingPickup || pushingObject)
        {
            if (!enemy)
            {
                prompt.SetActive(false);
                prompt.GetComponent<ParticleSystem>().Pause();
                if (promptPic.color.a > 0f)
                {
                    promptPic.color = new Color(promptPic.color.r, promptPic.color.g, promptPic.color.b, promptPic.color.a - (Time.deltaTime * 2));
                }
            }
        }

        if (holdingPickup && !transitioning)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Interact"))                     //Urgent -- Must Repair//////////////////////////////////////////////////
            {
                //lerpingPickup = false; 
                dropObject();
                
            }
        }

        if (pushingObject && !transitioning)
        {
            //print(pushPos.position);

            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Interact"))               //Urgent -- Must Repair//////////////////////////////////////////////////
            {
                pushingObject = false;
                //characterControl.pushing = false;          //Urgent - Must Repair ////////////////////////////////////////////////////////////////////////////
                //characterControl.lockMovement = false;     //Urgent - Must Repair ////////////////////////////////////////////////////////////////////////////

                objectToPush.GetComponent<Rigidbody>().mass = 100000;
                //gameObject.GetComponent<MoveBehaviour>().pushing = false;

                //Physics.IgnoreCollision(objectToPush.GetComponent<Collider>(), gameObject.transform.parent.GetComponent<Collider>(), false);
            }
            /*else                                             //Urgent - Must Repair ////////////////////////////////////////////////////////////////////////////
            {                                                                                 
                if (Vector3.Distance(transform.position, new Vector3(pushPoint.position.x, transform.position.y, pushPoint.position.z)) > 0.03f && characterControl.lockMovement)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(pushPoint.position.x, transform.position.y, pushPoint.position.z), 0.5f);
                    Physics.IgnoreCollision(objectToPush.GetComponent<Collider>(), gameObject.transform.GetComponent<Collider>());
                    //play moving anim here
                }
                else
                {                                                              
                    Physics.IgnoreCollision(objectToPush.GetComponent<Collider>(), gameObject.transform.GetComponent<Collider>(), false);
                    if (characterControl.lockMovement)
                    {
                        characterControl.lockMovement = false;
                        transform.LookAt(new Vector3(objectToPush.transform.position.x, transform.position.y, objectToPush.transform.position.z));
                        
                    }
                    else
                    {
                        transform.LookAt(new Vector3(objectToPush.transform.position.x, transform.position.y, objectToPush.transform.position.z));
                        if (Vector3.Distance(pushPos.position, new Vector3(pushPoint.position.x, pushPos.position.y, pushPoint.position.z)) > 1.5f)
                        {
                            pushingObject = false;
                            characterControl.pushing = false;
                            characterControl.lockMovement = false;
                            
                            objectToPush.GetComponent<Rigidbody>().mass = 100000;
                        }
                    }
                }
            }*/
        }

        if (holdingPickup && lerpingPickup)
        {
            objectToLift.transform.LookAt(currentLiftPos.position + transform.forward);
            objectToLift.transform.position = Vector3.Lerp(objectToLift.transform.position, currentLiftPos.position, liftSpeed*Time.deltaTime);
        }

        /*if (pushingObject)
        {
            //objectToPush.transform.position = Vector3.Lerp(objectToPush.transform.position, pushPos.position, 5);
        }*/

        transitioning = false;
    }
    public void LiftObject()
    {
        Invoke("finishLift", 1.3f);
        holdingPickup = true;
        lerpingPickup = true;
        objectToLift.GetComponent<Rigidbody>().useGravity = false;
        objectToLift.GetComponent<Rigidbody>().isKinematic = true;
        
        transitioning = true;
        objectToLift.GetComponent<LiftableObject>().beingCarried = true;
        objectToLift.GetComponent<LiftableObject>().Interactions = this;
        if (!enemy)
        {
            Physics.IgnoreCollision(objectToLift.GetComponent<Collider>(), gameObject.transform.GetComponent<Collider>());
        }
        /*if (characterControl != null)                            //Urgent - Must Repair ////////////////////////////////////////////////////////////////////////////
        {
            characterControl.lifting = true;
        }*/
    }

    void finishLift()
    {
        //holdingPickup = true;
        lerpingPickup = true;
        //objectToLift.GetComponent<Rigidbody>().useGravity = false;
        //objectToLift.GetComponent<Rigidbody>().isKinematic = true;
        currentLiftSpeed = 60f;
        //transitioning = true;
        //objectToLift.GetComponent<LiftableObject>().beingCarried = true;
        //objectToLift.GetComponent<LiftableObject>().Interactions = this;
        //Physics.IgnoreCollision(objectToLift.GetComponent<Collider>(), gameObject.transform.GetComponent<Collider>());
        //if (characterControl != null)
        //{
        //    characterControl.lifting = true;
        //}
    }

    public void dropObject()
    {
        holdingPickup = false;
        lerpingPickup = false;
        currentLiftSpeed = liftSpeed;
        objectToLift.GetComponent<Rigidbody>().isKinematic = false;
        objectToLift.GetComponent<Rigidbody>().useGravity = true;
        objectToLift.GetComponent<LiftableObject>().beingCarried = false;
        objectToLift.GetComponent<LiftableObject>().Interactions = null;
        if (!enemy)
        {
            Physics.IgnoreCollision(objectToLift.GetComponent<Collider>(), gameObject.transform.GetComponent<Collider>(), false);
        }
        /*if (characterControl != null)                                   //Urgent - Must Repair ////////////////////////////////////////////////////////////////////////////
        {
            characterControl.lifting = false;
        }*/
        CancelInvoke("finishLift");
    }
}
