using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    [SerializeField]
    private string objectiveName;
    [SerializeField]
    private bool objectiveComplete;

    public string ObjectiveName { get => objectiveName; set => objectiveName = value; }
    public bool ObjectiveComplete { get => objectiveComplete; set => objectiveComplete = value; }

    }
