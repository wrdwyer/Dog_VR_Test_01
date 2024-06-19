using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * This script helps people of crowd to panic and run to any exit and also die if needed.
 * This script will through error if there is no navmesh agent attached to the gameobject.
 * */
[RequireComponent(typeof(NavMeshAgent))]
public class CrowdResponce : MonoBehaviour {

    [SerializeField]
    float panicRunSpeed = 4;
    [SerializeField]
    float minPanicDistance = 3;
    [SerializeField]
    Animator animator = null;
    [SerializeField]
    string WalkAnimation = "Walk";
    [SerializeField]
    string PanicAnimation = "Walk";
    [SerializeField]
    string IdleAnimation = "Idle";
    [SerializeField]
    private Transform player = null;
    [SerializeField]
    private bool isPaniced = false;


    Transform []exitPonitsList;// Holds list of exit points in assending order
    Transform exitObj;
    bool exitSet;
    // Use this for initialization

    private void Awake()
        {
        player = GameObject.Find("Player").transform;

        }
    void Start () {
		
	}

    private void OnEnable()
    {
        
        PannicTrigger.triggerPanicAndRun += StartPanicAndRun;
    }

    private void OnDisable()
    {
        PannicTrigger.triggerPanicAndRun -= StartPanicAndRun;
    }

    // Update is called once per frame
    void Update ()
    {

        if (exitSet)
        {
            if (Vector3.Distance(exitObj.position, transform.position) < 1)
            {
                Debug.Log("Reached safely");
                exitSet = false;
                isPaniced = false;
                // Take some action as a persone has reached the exit safely.
            }
        }
        if (isPaniced && player != null)
        {
            transform.LookAt(player.transform);
        }

		
	}

    void StartPanicAndRun()
    {
        isPaniced = true;
        PanicExits exits = GameObject.Find("CrowdManager").GetComponent<PanicExits>();
        exitPonitsList = new Transform[exits.exitPoints.Length];
        animator.Play(PanicAnimation);
        // Debug.Log("exitPonitsList length = "+exitPonitsList.Length);
        // -----------------------------------------------------Sort all exit points distance -------------------------------------------------------------
        for (int i = 0; i < exits.exitPoints.Length; i++)
        {
            float distToExit = Vector3.Distance(transform.position,exits.exitPoints[i].position);
            if (i > 0)
            {
                //Debug.Log("Itteration " + i);
                for (int j = 0; j <= i; j++)
                {
                    //Debug.Log("Check for pos = "+j);
                    if (exitPonitsList[j] != null)
                    {
                        float distToOtherExit = Vector3.Distance(transform.position, exitPonitsList[j].position);
                        if (distToExit < distToOtherExit)
                        {
                            Transform tempHolder = exitPonitsList[j];
                            exitPonitsList[j] = exits.exitPoints[i];
                            Transform swaper;
                            while (j < i)
                            {
                                j++;
                                swaper = exitPonitsList[j];
                                exitPonitsList[j] = tempHolder;
                                tempHolder = swaper;
                               
                            }
                        }
                    }
                    else
                    {
                        exitPonitsList[j] = exits.exitPoints[i];// if Exit point list has empty slot at pos j, then just assign the transform.
                        break;
                    }
                }
            }
            else
            {
                exitPonitsList[0] = exits.exitPoints[i];
            }
        }
        // -----------------------------------------------------Sort all exit points distance(Over) -------------------------------------------------------------
        /*Debug.Log("-----------Exit points in assending order for--------------- "+gameObject.name);
        for (int i = 0; i < exitPonitsList.Length; i++)
        {
            Debug.Log(i+") "+ exitPonitsList[i].name);
        }*/
        SelectExitDestination();// Select the exit point to run to.
    }

    void SelectExitDestination()
    {
        GetComponent<Walk>().StopWalking();
        Transform panicObj = PannicTrigger.objTransform;
        float dist = Vector3.Distance(exitPonitsList[0].position,panicObj.position);
        //Debug.Log("Dist to " + exitPonitsList[0].name + " is = " + dist);
        if (dist > minPanicDistance)
        {
            exitObj = exitPonitsList[0];
            // if the closest exit point is at a safe distance from panic point then run to that exit.
        }
        else
        {
            exitObj = exitPonitsList[1];
            // Else run to next closest exit.
        }
        Vector3 dirToExit = exitObj.transform.position - transform.position;
        Vector3 dirToPanicObj = panicObj.transform.position - transform.position;
        float vecValue = Vector3.Dot(dirToExit.normalized, dirToPanicObj.normalized);
        //Debug.Log("Direction Vec: " + vecValue);
        if (vecValue > 0.7)
        {
            //Debug.Log("Choosing next Exit");
            exitObj = exitPonitsList[1];
        }
        //Debug.Log("Choose - "+exitObj.name);
        GetComponent<NavMeshAgent>().SetDestination(exitObj.position);
       
        exitSet = true;// So that in update we can check if this person has reached or not
        GetComponent<NavMeshAgent>().speed = panicRunSpeed;
        lookTowardsPlayer();
    }

    private void lookTowardsPlayer()
        {
        isPaniced = true;
        }

    private void OnCollisionEnter(Collision collision)
    {

        // Not active. Use it for detecting if there was a bullet hit.
        /*
         * if (collision.gameObject.tag == "bullet")
         {
             // That person dies
         }
         * */
    }
}
