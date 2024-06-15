using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class CarAIController : MonoBehaviour
{
    //Wheel transforms

    public Transform frontRight;
    public Transform frontLeft;
    public Transform rearRight;
    public Transform rearLeft;

    //Wheel colliders

    public WheelCollider frontRightCollider;
    public WheelCollider frontLeftCollider;
    public WheelCollider rearRightCollider;
    public WheelCollider rearLeftCollider;

    //Checkpoints
    [Tooltip("The checkpoint tranfrom that the ai checks for.")]
    public Transform nextCheckpoint;
    [Tooltip("The position where the ai will shoot a ray that will detect objects. Place it at the front of the car, outside the hitbox. The ai will ignore triggers.")]
    public Transform check;

    //Speed
    [Tooltip("The speed of the vehicle measured in km/h.")]
    public int kmh;
    [Tooltip("The speed limit applied to the ai to drive the car.")]
    public int speedLimit;

    private Stopwatch stopwatch = new Stopwatch();
    private Vector3 lastPos;
    private float steerAngle = 0f;

    //Car values

    public float acceleration = 100f;
    public float breaking = 1000f;
    [Tooltip("If its true, the AI will check for checkpoints.")]
    public bool CheckPointSearch = true;
    [Tooltip("If its true, it means an object is in front of the car.")]
    public bool objectDetected = false;
    [Tooltip("If its true, the vehicle will be controlled by the ai.")]
    public bool isCarControlledByAI = true;

    private void FixedUpdate()
    {
        WheelUpdate(frontRight, frontRightCollider);
        WheelUpdate(frontLeft, frontLeftCollider);
        WheelUpdate(rearRight, rearRightCollider);
        WheelUpdate(rearLeft, rearLeftCollider);

        //Calculate speed
        CalculateKMH();

        //Search for checkpoints

        SearchForCheckpoints();

    }

    private void WheelUpdate(Transform transform, WheelCollider collider)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }

    /// <summary>
    /// Accelerates the vehicle by the value given.
    /// </summary>
    public void Accelerate(float value)
    {
        frontRightCollider.motorTorque = value;
        frontLeftCollider.motorTorque = value;
    }

    /// <summary>
    /// Breaks the vehicle by the value given.
    /// </summary>
    public void Break(float value)
    {
        frontRightCollider.brakeTorque = value;
        frontLeftCollider.brakeTorque = value;
        rearRightCollider.brakeTorque = value;
        rearLeftCollider.brakeTorque = value;
    }

    /// <summary>
    /// Turns the front wheels at the angle given.
    /// </summary>
    public void Turn(float angle)
    {
        frontRightCollider.steerAngle = angle;
        frontLeftCollider.steerAngle = angle;
    }

    private void CalculateKMH()
    {
        if(stopwatch.IsRunning)
        {
            stopwatch.Stop();

            float distance = (transform.position - lastPos).magnitude;
            float time = stopwatch.Elapsed.Milliseconds / (float)1000;

            kmh = (int)((3600 * distance) / time / 1000);

            lastPos = transform.position;
            stopwatch.Reset();
            stopwatch.Start();

        }
        else
        {
            lastPos = transform.position;
            stopwatch.Reset();
            stopwatch.Start();
        }
    }

    /// <summary>
    /// Sets the speed of the vehicle to the one given in the parameter.
    /// </summary>
    public void SetSpeed(int speedLimit)
    {
        if (kmh > speedLimit)
        {
            Break(breaking);
            Accelerate(0);
        }
        else if (kmh < speedLimit)
        {
            Accelerate(acceleration);
            Break(0);
        }
    }

    private void SearchForCheckpoints()
    {
        if (CheckPointSearch && isCarControlledByAI)
        {
            Vector3 nextCheckpointRelative = transform.InverseTransformPoint(nextCheckpoint.position);

            steerAngle = (nextCheckpointRelative.x / nextCheckpointRelative.magnitude);
            float xangle = (nextCheckpointRelative.y / nextCheckpointRelative.magnitude);

            steerAngle = (Mathf.Asin(steerAngle) * 180) / 3.14f;
            xangle = (Mathf.Asin(xangle) * 180) / 3.14f;

            Turn(steerAngle);

            check.localRotation = Quaternion.Euler(-xangle, steerAngle, 0);

            float maxDistance = kmh * 2/5 + 2f;

            RaycastHit carHit;

            bool searchForOtherCars = Physics.Raycast(check.position, check.forward, out carHit, maxDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            UnityEngine.Debug.DrawRay(check.position, check.forward * maxDistance, Color.green);
           
            if (searchForOtherCars && carHit.transform != transform)
            {
                SetSpeed(0);
                objectDetected = true;
            }
            else
            {
                SetSpeed(speedLimit);
                objectDetected = false;
            }
        }
    }
}