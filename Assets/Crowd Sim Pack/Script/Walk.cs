using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * This script helps people of crowd to walk around.
 * This script will through error if there is no navmesh agent attached to the gameobject.
 * */
[RequireComponent(typeof(NavMeshAgent))]
public class Walk : MonoBehaviour {
    public float walkSpeed;
    public Transform[] wps;
    
    Transform currentWp;
    int WpIndex;
    NavMeshAgent navAgent;
    bool canWalk;
	
	void Start () {

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = walkSpeed;
        WpIndex = 0;
        currentWp = wps[WpIndex++];
        navAgent.SetDestination(currentWp.position);
        canWalk = true; // People cannot keep walking after some kind of event, like bomb blast. They must panic and run.

    }
	
	
	void Update () {

        if (!canWalk)// This portion must be exicuted when people are walking according to waypoints assigned. Until panic.
        {
            return;
        }
        if (Vector3.Distance(currentWp.position, transform.position) < 1)
        {
            NavMeshPath navMeshPath = new NavMeshPath();
            navAgent.CalculatePath( wps[WpIndex].position, navMeshPath);
            if (navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                //Debug.Log("Path Exists");
                currentWp = wps[WpIndex++];
                navAgent.SetDestination(currentWp.position);// update destination to next way point.
                if (WpIndex == wps.Length) // if this is the last point then go to first point.
                {
                    WpIndex = 0;
                }
            }
            else // if there is no way to reach next
            {
                WpIndex += 2;
                if (WpIndex == wps.Length)
                {
                    WpIndex = 0;
                }
                //Debug.Log("Path does not Exists");
                navMeshPath = new NavMeshPath();
                navAgent.CalculatePath(wps[WpIndex].position, navMeshPath);
                if (navMeshPath.status == NavMeshPathStatus.PathComplete)
                {
                    currentWp = wps[WpIndex++];
                    navAgent.SetDestination(currentWp.position);// if the next way point cannot be reached then skip it.
                }

            }
        }
	}

    public void StopWalking() // Call when people must run or die or other things, so they must stop walking.
    {
        canWalk = false;
        // Impliment other logic that you may need, like changing audio.
    }
}
