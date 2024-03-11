using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContinuousMovementPhysics : MonoBehaviour
{
    public float speed =1f;
    public InputActionProperty moveInputSource;
    public Rigidbody rb;
    public Transform directionSource;
    private Vector2 inputMoveAxis;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputMoveAxis = moveInputSource.action.ReadValue<Vector2>();

    }
    void FixedUpdate() 
    {
        Quaternion yaw = Quaternion.Euler(0, directionSource.eulerAngles.y, 0);
        Vector3 direction = yaw * new Vector3(inputMoveAxis.x, 0, inputMoveAxis.y);
        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime * speed);
    }
}
