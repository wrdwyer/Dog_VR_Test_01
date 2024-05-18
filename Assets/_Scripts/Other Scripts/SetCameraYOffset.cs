using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class SetCameraYOffset : MonoBehaviour
{
    /// <summary>
    /// Set the player Height for Standing and Crouching.
    /// </summary>
    
    [Tooltip("Default y offset for camera")]
    [SerializeField]
    private float yOffsetDefault = 0.5f;
    
    
    [Tooltip("y offset for Crouch")]
    [SerializeField]
    private float yCrouch = 0.3f;

    private XROrigin xROrigin;
    private GameObject CameraFloorOffsetObject;
    private Transform cameraTransform;

    void OnEnable()
    {
         xROrigin = GetComponent<XROrigin>();
         xROrigin.CameraYOffset = yOffsetDefault; 
         xROrigin.CameraFloorOffsetObject.gameObject.transform.localPosition = new Vector3(0, yOffsetDefault,0 );
           
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
