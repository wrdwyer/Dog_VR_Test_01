using UnityEngine;

public class DrawBoxGizmo : MonoBehaviour
    {
    // Set the size of the gizmo box
    public Vector3 gizmoSize = new Vector3(1, 1, 1);
    // Set the color of the gizmo box
    public Color gizmoColor = Color.yellow;

    void OnDrawGizmos()
        {
        // Set the gizmo color
        Gizmos.color = gizmoColor;
        // Draw a wireframe cube at the GameObject's position with the specified size
        Gizmos.DrawWireCube(transform.position, gizmoSize);
        }
    }

