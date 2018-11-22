using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpen2 : MonoBehaviour {

    public Image FillBar1;
    public GameObject FillRing;
    public ParticleSystem LightParts;

    public Transform Top1, Top2;
    public Transform Mid1, Mid2;
    public Transform Back1, Back2;

    Transform LerpTop1, LerpTop2;
    Transform LerpMid1, LerpMid2, LerpBack1, LerpBack2;
    public Transform LerpBlock1/*, LerpBlock2*/;

    public float DoorSpeed = 0.1f;
    public float ShutterSpeed = 0.1f;
    public float FillSpeed = 0.1f;
    public float CircFillSpeed = 0.1f;
    public float BlockSpeed = 0.1f;
    public float ShutterDelay = 0.15f;
    public float doorDelay = 0.15f;

    public bool barActivated = false;
    public LoadingBar bar;

    AudioSource doorSound;

    Transform Block;

    [HideInInspector]
    public bool BlockFound = false;
    bool Activated = false;
    bool TopOpening = false;
    bool TopOpen = false;
    bool MidOpening = false;
    bool MidOpen = false;
    bool BackOpening = false;
    bool BackOpen = false;
    public AudioSource d2;
    public Light sphereLight;
    public float maxSphereLightInt = 4;

    bool firstShutterSoundPlayed = false;
    bool secondShutterSoundPlayed = false;


    // Use this for initialization
    void Start() {
        //FillSpeed = FillSpeed / 100;
        //WingSpeed = WingSpeed / 100;
        //DoorSpeed = DoorSpeed / 100;
        BlockSpeed = BlockSpeed * 100;
        doorSound = gameObject.GetComponent<AudioSource>();

        LerpTop1 = Instantiate(LerpBlock1, Vector3.zero, Quaternion.identity);
        LerpTop2 = Instantiate(LerpBlock1, Vector3.zero, Quaternion.identity);
        LerpBack1 = Instantiate(LerpBlock1, Vector3.zero, Quaternion.identity);
        LerpBack2 = Instantiate(LerpBlock1, Vector3.zero, Quaternion.identity);
        LerpMid1 = Instantiate(LerpBlock1, Vector3.zero, Quaternion.identity);
        LerpMid2 = Instantiate(LerpBlock1, Vector3.zero, Quaternion.identity);

        LerpTop1.position = new Vector3(Top1.position.x, Top1.position.y + 4.9f, Top1.position.z);
        LerpTop2.position = new Vector3(Top2.position.x, Top2.position.y - 3.75f, Top2.position.z);

        LerpTop1.position = Top1.position + (Vector3.up * 4.9f) - (transform.up * 1.5f);
        LerpTop2.position = Top2.position - (Vector3.up * 3.75f) - (transform.up * 1.5f);

        //LerpMid1.position = new Vector3(Mid1.position.x - 2f, Mid1.position.y, Mid1.position.z);
        //LerpMid2.position = new Vector3(Mid2.position.x + 2f, Mid2.position.y, Mid2.position.z);

        LerpMid1.position = Mid1.position - (transform.right * 3);
        LerpMid2.position = Mid2.position + (transform.right * 3);

        LerpBack1.position = new Vector3(Back1.position.x, Back1.position.y - 3.75f, Back1.position.z);
        LerpBack2.position = new Vector3(Back2.position.x, Back2.position.y + 4f, Back2.position.z);
    }

    // Update is called once per frame
    void Update() {

        /*if (Input.GetKeyDown(KeyCode.G))
        {
            Activated = true;
        }*/

        if (BlockFound && !Activated && !TopOpen && !barActivated)
        {
            Block.gameObject.GetComponent<Rigidbody>().useGravity = false;
            Block.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Block.gameObject.GetComponent<Animator>().enabled = false;
            Block.position = Vector3.Lerp(Block.position, LerpBlock1.position, (BlockSpeed * Time.deltaTime));
            //Block.eulerAngles = Vector3.Lerp(Block.eulerAngles, new Vector3(0,0,0), 0.01f);
            Block.rotation = Quaternion.Slerp(Block.rotation, Quaternion.identity, 0.05f);
            //FillRing.SetActive(true);
            //if (FillRing.transform.localScale.x < 17.66f && Vector3.Distance(Block.transform.position, LerpBlock1.position) < 0.5f)
            //{
            //    FillRing.transform.localScale = new Vector3(FillRing.transform.localScale.x + (CircFillSpeed * Time.deltaTime),
            //        FillRing.transform.localScale.y + (CircFillSpeed * Time.deltaTime), FillRing.transform.localScale.z + (CircFillSpeed * Time.deltaTime));
            //}
            if (sphereLight != null)
            {
                if (sphereLight.intensity < maxSphereLightInt)
                {
                    sphereLight.intensity += 1.5f * Time.deltaTime;
                }
            }
            if (Vector3.Distance(Block.position, LerpBlock1.position) < 0.05f && Quaternion.Angle(Block.rotation, Quaternion.identity) < 1.5f)
            {
                Block.parent = Top1;
                Activated = true;
            }
        }

        if (barActivated)
        {
            if (bar.currentFillNum > 0.97f)
            {
                Activated = true;
            }
        }

        if (Activated)
        {
            if (!barActivated)
            {
                if (sphereLight != null)
                {
                    if (sphereLight.intensity < maxSphereLightInt)
                    {
                        sphereLight.intensity += 1.5f*Time.deltaTime;
                    }
                }

                if (FillRing.transform.localScale.x < 17.66f && Vector3.Distance(Block.transform.position, LerpBlock1.position) < 0.5f)
                {
                    FillRing.transform.localScale = new Vector3(FillRing.transform.localScale.x + (10 * Time.deltaTime),
                        FillRing.transform.localScale.y + (10 * Time.deltaTime), FillRing.transform.localScale.z + (10 * Time.deltaTime));
                }
                else
                {
                    if ((FillBar1.fillAmount < 1) && !TopOpening)
                    {
                        FillBar1.fillAmount += (FillSpeed * Time.deltaTime);
                    }
                    else if (TopOpening == false)
                    {
                        Invoke("OpenTop", doorDelay);
                    }
                }
            }
            else
            {
                if (FillBar1.fillAmount < 1)
                {
                    FillBar1.fillAmount += (FillSpeed * Time.deltaTime);
                }
                else
                {
                    FillRing.SetActive(true);

                    if (FillRing.transform.localScale.x < 17.66f && !TopOpening)
                    {
                        FillRing.transform.localScale = new Vector3(FillRing.transform.localScale.x + (CircFillSpeed * Time.deltaTime),
                            FillRing.transform.localScale.y + (CircFillSpeed * Time.deltaTime), FillRing.transform.localScale.z + (CircFillSpeed * Time.deltaTime));
                    }
                    else if (TopOpening == false)
                    {
                        Invoke("OpenTop", doorDelay);
                    }
                }
            }


            if (TopOpening && !TopOpen)
            {
                Top1.position = Vector3.MoveTowards(Top1.position, LerpTop1.position, (DoorSpeed * Time.deltaTime));
                Top2.position = Vector3.MoveTowards(Top2.position, LerpTop2.position, (DoorSpeed * Time.deltaTime));
                /*if (!barActivated)
                {
                    if (Vector3.Distance(Block.position, LerpBlock2.position) > 0.03f)
                    {
                        Block.position = Vector3.Lerp(Block.position, LerpBlock2.position, ((BlockSpeed * Time.deltaTime) / 10));
                    }
                }*/
                if (Vector3.Distance(Top1.position, LerpTop1.position) <= DoorSpeed && Vector3.Distance(Top2.position, LerpTop2.position) <= DoorSpeed
                    && (barActivated || (Block != null && Vector3.Distance(Block.position, LerpBlock1.position) < 0.6f)))
                {
                    TopOpen = true;
                    Invoke("OpenMid", ShutterDelay);
                }
            }

            if (MidOpening && !MidOpen)
            {
                if (d2 != null && firstShutterSoundPlayed == false)
                {
                    d2.Play();
                    firstShutterSoundPlayed = true;
                }
                Mid1.position = Vector3.MoveTowards(Mid1.position, LerpMid1.position, (ShutterSpeed * Time.deltaTime));
                Mid2.position = Vector3.MoveTowards(Mid2.position, LerpMid2.position, (ShutterSpeed * Time.deltaTime));

                if (Vector3.Distance(Mid1.position, LerpMid1.position) < 0.1f && Vector3.Distance(Mid2.position, LerpMid2.position) < 0.1f)
                {
                    MidOpen = true;
                    Invoke("OpenBack", ShutterDelay);
                }
            }

            if (BackOpening && !BackOpen)
            {
                if (d2 != null && secondShutterSoundPlayed == false)
                {
                    d2.Play();
                    secondShutterSoundPlayed = true;
                }
                Back1.position = Vector3.MoveTowards(Back1.position, LerpBack1.position, (ShutterSpeed * 2 * Time.deltaTime));
                Back2.position = Vector3.MoveTowards(Back2.position, LerpBack2.position, (ShutterSpeed * 2 * Time.deltaTime));

                if (Vector3.Distance(Back1.position, LerpBack1.position) < 0.1f && Vector3.Distance(Back2.position, LerpBack2.position) < 0.1f)
                {
                    BackOpen = true;
                    Activated = false;
                }
            }

        }
    }

    void OpenTop()
    {
        if (!TopOpening)
        {
            TopOpening = true;
            LightParts.gameObject.SetActive(true);
            LightParts.Play();
            doorSound.Play();
            Invoke("HideBars", 0.15f);
            Invoke("DestroyParts", 4);
        }
    }

    void OpenMid()
    {
        MidOpening = true;
    }

    void OpenBack()
    {
        BackOpening = true;
    }

    //void OpenDoor()
    //{
    //    DoorOpening = true;
    //}

    void HideBars()
    {
        FillBar1.gameObject.SetActive(false);
    }

    void DestroyParts()
    {
        LightParts.Stop();
        LightParts.gameObject.SetActive(false);
    }

    void OnTriggerStay(Collider hit)
    {
        if (!BlockFound && !barActivated)
        {
            if (hit.gameObject.tag == "EnergySource")
            {
                Block = hit.transform;
                BlockFound = true;
                Block.gameObject.GetComponent<Rigidbody>().useGravity = false;
                Block.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                Block.gameObject.GetComponent<BoxCollider>().enabled = false;
                Block.gameObject.GetComponent<LiftableObject>().Interactions.dropObject();
                Block.gameObject.GetComponent<LiftableObject>().lightProjector.enabled = false;
                Block.gameObject.GetComponent<LiftableObject>().enabled = false;
                Block.gameObject.GetComponent<Outline>().enabled = false;
                Block.tag = "Untagged";
            }
        }
    }
}
