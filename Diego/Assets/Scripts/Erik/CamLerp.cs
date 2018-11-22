using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLerp : MonoBehaviour {

    public Transform[] lerpPoints;
    public float[] delays;
    public float lerpSpeed;
    public float slerpSpeed;
    float timer = 0;
    int currentPos = 0;
    public bool lerping = false;
    public bool loop = false;
    public bool useDelays = true;
    bool active = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (useDelays)
        {
            if (active)
            {
                if (lerping)
                {
                    transform.position = Vector3.Lerp(transform.position, lerpPoints[currentPos].position, lerpSpeed * Time.deltaTime);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lerpPoints[currentPos].rotation, slerpSpeed * Time.deltaTime);
                    if ((Vector3.Distance(transform.position, lerpPoints[currentPos].position) < 0.05f) &&
                    (Quaternion.Angle(transform.rotation, lerpPoints[currentPos].rotation) < 0.5f))
                    {
                        lerping = false;
                    }
                }
                else
                {
                    if (timer >= delays[currentPos])
                    {
                        switchPos();
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
                }
            }
        }
        else
        {
            if (lerping)
            {
                transform.position = Vector3.Lerp(transform.position, lerpPoints[currentPos].position, lerpSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, lerpPoints[currentPos].rotation, slerpSpeed * Time.deltaTime);
                if ((Vector3.Distance(transform.position, lerpPoints[currentPos].position) < 0.05f) &&
                (Quaternion.Angle(transform.rotation, lerpPoints[currentPos].rotation) < 0.5f))
                {
                    lerping = false;
                }
            }
        }
    }

    void switchPos()
    {
        timer = 0;
        lerping = true;
        if (currentPos < lerpPoints.Length-1)
        {
            currentPos += 1;
        }
        else
        {
            if (loop)
            {
                currentPos = 0;
            }
            else
            {
                active = false;
            }
        }
    }

    public void switchPos(int changeAmount)
    {
        lerping = true;
        currentPos = (currentPos + changeAmount) % lerpPoints.Length;
    }
}
