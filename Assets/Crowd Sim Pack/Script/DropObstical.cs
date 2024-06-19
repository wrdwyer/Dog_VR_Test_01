using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Drop an obstical at mouse location, on right click.
 * */
public class DropObstical : MonoBehaviour {

    public Camera cam;
    public GameObject obstical;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(1))// Right click to drop the obstical at mouse location
        {
           
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = cam.gameObject.transform.position.y;
            Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(mousePos);
            worldCoordinates.y = 5;
            Debug.Log("World Coordinates = " + worldCoordinates);
            obstical.transform.position = worldCoordinates;
        }

    }
}
