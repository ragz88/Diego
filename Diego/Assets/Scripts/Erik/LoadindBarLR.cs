using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadindBarLR : MonoBehaviour {

    public enum BarState
    {
        Filling,
        Emptying,
        Full,
        Empty
    }

    public BarState barState = BarState.Empty;

    LineRenderer line;
    public Transform[] movePoints;
    int currentForward = 1;
    int currentBackward = 0;
    
    public float speed = 1;
    public GameObject currentLoadPosPrefab;
    Transform currentLoadPos;
    Vector3[] linePoints;

    CapsuleCollider[] capsules;

    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
        currentLoadPos = Instantiate(currentLoadPosPrefab, movePoints[0].position, Quaternion.identity).transform;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F))
        {
            barState = BarState.Filling;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            barState = BarState.Emptying;
        }

        if (barState == BarState.Filling)
        {
            if (Vector3.Distance(movePoints[currentForward].position, currentLoadPos.position) > 0.02f)
            {
                currentLoadPos.position = Vector3.MoveTowards(currentLoadPos.position, movePoints[currentForward].position, speed * Time.deltaTime);
            }
            else
            {
                if (currentForward < movePoints.Length - 1)
                {
                    currentForward++;
                    currentBackward++;
                }
                else
                {
                    barState = BarState.Full;
                }
            }


            linePoints = new Vector3[currentForward + 1];
            line.positionCount = currentForward + 1;
            for (int i = 0; i <= currentForward; i++)
            {
                if (i < currentForward)
                {
                    linePoints[i] = movePoints[i].position;
                }
                else
                {
                    linePoints[i] = currentLoadPos.position;
                }
            }

            line.SetPositions(linePoints);
        }
        else if (barState == BarState.Emptying)
        {
            if (Vector3.Distance(movePoints[currentBackward].position, currentLoadPos.position) > 0.02f)
            {
                currentLoadPos.position = Vector3.MoveTowards(currentLoadPos.position, movePoints[currentBackward].position, speed * Time.deltaTime);
            }
            else
            {
                if (currentBackward > 0)
                {
                    currentForward--;
                    currentBackward--;
                }
                else
                {
                    barState = BarState.Empty;
                }
            }


            linePoints = new Vector3[currentForward + 1];
            line.positionCount = currentForward + 1;
            for (int i = 0; i <= currentForward; i++)
            {
                if (i < currentForward)
                {
                    linePoints[i] = movePoints[i].position;
                }
                else
                {
                    linePoints[i] = currentLoadPos.position;
                }
            }

            line.SetPositions(linePoints);
        }
	}
}
