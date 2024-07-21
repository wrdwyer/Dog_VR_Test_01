using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogVR;

public class SetNewObjective : MonoBehaviour
{
    public int newObjectiveIndex;
    public bool useCollider = false;
    private bool triggerSet = false;

    private void OnTriggerEnter(Collider other)
        {
        if (other.gameObject.CompareTag("Player") && useCollider && !triggerSet)
            {
            SetNewObjectiveGO(newObjectiveIndex);
            triggerSet = true;
            }
        
        }
    public void SetNewObjectiveGO(int newObjectiveIndex)
        {
        GameManager.Instance.SetObjectivesManager.currentObjectiveSO.objectiveComplete = true;
        GameManager.Instance.SetObjectivesManager.CurrentObjectiveIndex = newObjectiveIndex;
        }
    }