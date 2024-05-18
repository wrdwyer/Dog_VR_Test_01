using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumpv2 : MonoBehaviour
{
  [SerializeField] private InputActionProperty jumpButton;
  [SerializeField] private float jumpHeight;
  [SerializeField] private CharacterController characterController;
  [SerializeField] private LayerMask groundLAyer;
  private float gravity = Physics.gravity.y;
  private Vector3 movement;
  // Check if the object is grounded by performing a sphere cast at its position with the specified radius and layer mask.
  private void Update() 
  {
    bool _isGrounded = IsGrounded();
    
    if (jumpButton.action.WasPressedThisFrame() && _isGrounded)
    {
      Jump();
    }
    movement.y += gravity * Time.deltaTime;
    characterController.Move(movement * Time.deltaTime);
  }

    private void Jump()
    {
      movement.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

    private bool IsGrounded()
  { 
    return characterController.isGrounded;
    //Physics.CheckSphere(transform.position, 0.2f, groundLAyer);
  }
  

}
