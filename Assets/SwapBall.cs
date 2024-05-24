using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentMesh : MonoBehaviour
    {
    [SerializeField] public GameObject Object;
    [SerializeField] public GameObject newParent;

    public void Unparent()
        {
        if (Object.transform.parent != null)
            {
            //Vector3 currentWorldPos = Object.transform.position;
            Object.transform.SetParent(newParent.transform, false);
            //Debug.Log(currentWorldPos);
            //Object.transform.position = currentWorldPos;
            //Object.transform.parent = null;
            }
        }
    }
