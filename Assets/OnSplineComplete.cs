using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DogVR;

public class OnSplineComplete : MonoBehaviour
    {
    public SplineAnimate splineAnimate;
    public Animator animator;
    public StudioEventEmitter StudioEventEmitter;  



    private void Start()
        {
        splineAnimate = GetComponent<SplineAnimate>();
        animator = GetComponent<Animator>();
        splineAnimate.Completed += () => SplineCompleted();
        }     

    private void SplineCompleted()
        {
        animator.Play("Idle");
        StudioEventEmitter.Play();
        Debug.Log("Spline Completed");
        SetNewObjective newObjective = GetComponent<SetNewObjective>();
        newObjective.SetNewObjectiveGO(2);
        splineAnimate.Completed -= () => SplineCompleted();
        }


  

    }
