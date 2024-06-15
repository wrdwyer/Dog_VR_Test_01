using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopScript : MonoBehaviour
{
    [Tooltip("If true the car that touches the trigger will stop.")]
    public bool stop = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        CarAIController carController = other.gameObject.GetComponent<CarAIController>();

        if (carController != null)
        {
            if (stop && !carController.objectDetected)
            {
                carController.SetSpeed(0);
                carController.CheckPointSearch = false;
            }

            if (!stop && !carController.objectDetected)
            {
                carController.SetSpeed(carController.speedLimit);
                carController.CheckPointSearch = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        CarAIController carController = other.gameObject.GetComponent<CarAIController>();

        if(carController != null)
        {
            carController.SetSpeed(carController.speedLimit);
            carController.CheckPointSearch = true;
        }

    }
}
