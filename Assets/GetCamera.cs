using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogVR;

public class GetCamera : MonoBehaviour
    {
    public Camera cam;
    public Canvas canvas;
    public float planeDistance = 3f;
    void Start()
        {
        
        cam = GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<Camera>();
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = cam;
        canvas.planeDistance = planeDistance;
        }

    }
