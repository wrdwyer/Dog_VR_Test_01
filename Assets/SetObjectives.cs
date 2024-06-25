using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DogVR;
using DogVR.Actions;

public class SetObjectives : MonoBehaviour
    {
    public CurrentObjectiveSO currentObjectiveSO;
    [SerializeField]
    private int currentObjectiveIndex = 0;
    public List<GameObject> objectivesList;
    public DrawPathToObjective drawPathToObjective;

   
    public int CurrentObjectiveIndex
    {
        get
        {
            return currentObjectiveIndex;
        }
        set
        {
            currentObjectiveIndex = value;
            SetObjective();
            drawPathToObjective.UpdateCurrentObjective();
            Debug.Log("Current Objective Index: " + currentObjectiveIndex);
        }

    }

    [Button("Set Objective")]
    public void SetObjective()
    {
        currentObjectiveSO.CurrentObjective = objectivesList[currentObjectiveIndex];
        currentObjectiveSO.ObjectiveName = objectivesList[currentObjectiveIndex].GetComponent<Objective>().ObjectiveName;
        currentObjectiveSO.ObjectiveComplete = objectivesList[currentObjectiveIndex].GetComponent<Objective>().ObjectiveComplete;
    }

    /*[Button("Set Objective")]
    public void SetAllObjectives(GameObject objectiveGameobject, string objectiveName, bool completed)
    {
        
        currentObjectiveSO.currentObjective = objectiveGameobject;
        currentObjectiveSO.objectiveName = objectiveName;
        currentObjectiveSO.objectiveComplete = completed;
    }*/

}
