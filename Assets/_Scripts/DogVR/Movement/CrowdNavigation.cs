using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowdNavigation : MonoBehaviour
    {
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    //private GameObject Path;
   // private Transform[] pathPoints;
    public List<Transform> pathPoints = new List<Transform>();

    [SerializeField]
    private float minDistance = 10f;
    private int index = 0;

    private void Start()
        {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //pathPoints = new Transform[Path.transform.childCount];

        }

    private void Update()
        {
        Roam();
        }
    private void Roam()
        {
        if (Vector3.Distance(transform.position, pathPoints[index].position) < minDistance)
            {
            index = (index + 1) % pathPoints.Count;
            }

        agent.SetDestination(pathPoints[index].position);
        animator.SetFloat("Vertical", !agent.isStopped ? 1f : 0f);
        }

    }
    
