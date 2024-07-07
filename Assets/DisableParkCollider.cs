using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableParkCollider : MonoBehaviour
    {



    void OnCollisionStay(Collision collision)
        {

        foreach (ContactPoint contact in collision.contacts)
            {
            Debug.Log("Objects touching: " + contact.otherCollider.name);
            if (collision.contacts.Length == 1 && collision.gameObject.tag == "Player")
                {
                DisableParkColliderWall();
                Debug.Log("Collider is empty");
                }
            }

        }
    public void DisableParkColliderWall()
        {
        GetComponent<BoxCollider>().enabled = false;
        Debug.Log("Disabled Collider by Bark");
        }

    }
