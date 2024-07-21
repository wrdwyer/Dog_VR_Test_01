using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxiousDogAnimation : MonoBehaviour
    {


    private void OnTriggerExit(Collider other)
        {
        if (other.gameObject.tag == "Hoodie")
            {
            GetComponentInParent<BlazeAI>().enabled = true;
            Animator animator = GetComponentInParent<Animator>();
            animator.SetFloat("Movement_f", 0.5f);
            GetComponent<SphereCollider>().enabled = false;
            Debug.Log("Hoodie Triggered");
            }
        }
    }
