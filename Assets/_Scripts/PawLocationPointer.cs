using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawLocationPointer : MonoBehaviour
{
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;

    private void Awake()
        {
        targetPosition = new Vector3(200,450);
        pointerRectTransform = GetComponent<RectTransform>();

        }
    }
