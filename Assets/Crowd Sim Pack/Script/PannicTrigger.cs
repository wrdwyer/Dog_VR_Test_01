using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix;
using Sirenix.OdinInspector;

/*
 * Helps to switch Cam and drop the pannic object/trigger
 * */
public class PannicTrigger : MonoBehaviour {

    public static UnityAction triggerPanicAndRun;

    //public GameObject mainCam;
    //public GameObject secondCam;
    //public Text log;
    [HideInInspector]
    public static Transform objTransform;

    bool readyToDrop;
    // Use this for initialization
    void Start() {
        objTransform = this.transform;

        }

    // Update is called once per frame
    /*void Update() {


        if (Input.GetKeyDown(KeyCode.S)) // Switch between cameras.
            {
            if (secondCam.activeInHierarchy)
                {
                secondCam.SetActive(false);
                mainCam.SetActive(true);
                }
            else
                {
                secondCam.SetActive(true);
                mainCam.SetActive(false);
                }
            }

        if (Input.GetKeyDown(KeyCode.D)) // You must hold 'D' to drop the pannic trigger.
            {
            //Debug.Log("Got D");
            //log.text = "Ready";
            readyToDrop = true;
            }
        if (Input.GetKeyUp(KeyCode.D))
            {
            //Debug.Log("Got D");
            //log.text = "Not ready";
            readyToDrop = false;
            }
        if (readyToDrop)
            {

            if (Input.GetMouseButton(0))// Drop the panic object, like a bomb or plane.
                {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = mainCam.transform.position.y;
                Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(mousePos);
                worldCoordinates.y = 5;
                Debug.Log("World Coordinates = " + worldCoordinates);
                transform.position = worldCoordinates;
                GetComponent<Rigidbody>().useGravity = true;
                }

            }

        }*/
    [Button ("Panic Event")]
    public void PanicEventLive()
    {
        Debug.Log("PanicEventLive");
        Debug.Log("Calling Event");
        triggerPanicAndRun();
       
    }

}
