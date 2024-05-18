//Code based on video by Black Whale Studio
//https://www.youtube.com/watch?v=-jh-YlRXuyk&list=PLTAA462ffL32Fad2I-ev2sgWaFAEUqcd4&index=32&t=315s

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpButton;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private CharacterController characterController;
    private Vector3 _playerVelocity;

    private void OnEnable() 
    {
        jumpButton.action.performed += Jumping;    
    }   

    private void OnDisable() 
    {
        jumpButton.action.performed -= Jumping; 
    }

    private void Jumping(InputAction.CallbackContext obj)
    {
        if(!characterController.isGrounded) return;
        _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue); //jump jumpHeight;
    }

    private void Update()
    {
        if(characterController.isGrounded && _playerVelocity.y < 0.0f)
        {
            _playerVelocity.y = 0.0f;
        }
        _playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(_playerVelocity * Time.deltaTime);
    }

}
