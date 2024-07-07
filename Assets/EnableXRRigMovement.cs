using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using DogVR;
using UnityEngine.XR.Interaction.Toolkit;

public class EnableXRRigMovement : MonoBehaviour
{
 public void EnableMovement()
        {

        DisableTeleportComponents disableTeleportComponents = GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<DisableTeleportComponents>();
        if (disableTeleportComponents != null)
            {
            disableTeleportComponents.enabled = false;
            Debug.Log("DisableTeleportComponents is disabled");
            }
        /*
        DynamicMoveProvider dynamicMoveProvider = GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<DynamicMoveProvider>();
        TeleportationProvider teleportationProvider= GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<TeleportationProvider>();
        
        // Check if the DynamicMoveProvider component is not null
        if (dynamicMoveProvider != null)
            {
            // Enable the DynamicMoveProvider component
            dynamicMoveProvider.enabled = true;
            Debug.Log("Dynamic Move Provider Enabled");
            }

        // Check if the TeleportationProvider component is not null
        if (teleportationProvider != null)
            {
            // Enable the TeleportationProvider component
            teleportationProvider.enabled = true;
            Debug.Log("Teleportation Provider Enabled");
            }*/

        }
}
