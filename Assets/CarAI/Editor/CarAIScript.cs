using System;
using System.Diagnostics.Eventing.Reader;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CarAIScript : EditorWindow
{
    private bool setupcar = false;
    private bool spawncheckpoints = false;
    private bool createintersection = false;

    //Car setup

    private GameObject carmodel;

    private Transform frontRight;
    private Transform frontLeft;
    private Transform rearRight;
    private Transform rearLeft;

    private WheelCollider frontRightCollider;
    private WheelCollider frontLeftCollider;
    private WheelCollider rearLeftCollider;
    private WheelCollider rearRightCollider;

    private float acceleration = 10000;
    private float breaking = 100000;
    private int speedLimit;
    private Transform check;

    //Checkpoint spawner

    private GameObject currentGameObject;
    private int checkpointSpeedLimit;
    private Vector3 checkpointScale;

    //Intersection creator
    private int stops = 0;


    [MenuItem("Window/Car AI")]

    public static void ShowWindow()
    {
        GetWindow<CarAIScript>("Car AI");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Setup car"))
        {
            setupcar = true;
            spawncheckpoints = false;
            createintersection = false;
        }
        else if (GUILayout.Button("Spawn checkpoints"))
        {
            spawncheckpoints = true;
            setupcar = false;
            createintersection = false;
        }
        else if(GUILayout.Button("Create intersection"))
        {
            createintersection = true;
            setupcar = false;
            spawncheckpoints = false;
        }

        if (setupcar)
        {
            SetupCar();
        }
        else if (spawncheckpoints)
        {
            SpawnCheckpoints();
        }
        else if(createintersection)
        {
            CreateIntersection();
        }

    }

    void SetupCar()
    {
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Car model", EditorStyles.boldLabel);
        carmodel = (GameObject)EditorGUILayout.ObjectField(carmodel, typeof(GameObject), true);

        EditorGUILayout.EndHorizontal();



        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Front right wheel transform", EditorStyles.boldLabel);
        frontRight = (Transform)EditorGUILayout.ObjectField(frontRight, typeof(Transform), true);

        EditorGUILayout.EndVertical();



        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Front left wheel transform", EditorStyles.boldLabel);
        frontLeft = (Transform)EditorGUILayout.ObjectField(frontLeft, typeof(Transform), true);

        EditorGUILayout.EndVertical();



        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Rear right wheel transform", EditorStyles.boldLabel);
        rearRight = (Transform)EditorGUILayout.ObjectField(rearRight, typeof(Transform), true);

        EditorGUILayout.EndVertical();



        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Rear left wheel transform", EditorStyles.boldLabel);
        rearLeft = (Transform)EditorGUILayout.ObjectField(rearLeft, typeof(Transform), true);

        EditorGUILayout.EndVertical();



        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Front right wheel collider", EditorStyles.boldLabel);
        frontRightCollider = (WheelCollider)EditorGUILayout.ObjectField(frontRightCollider, typeof(WheelCollider), true);

        EditorGUILayout.EndVertical();



        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Front left wheel collider", EditorStyles.boldLabel);
        frontLeftCollider = (WheelCollider)EditorGUILayout.ObjectField(frontLeftCollider, typeof(WheelCollider), true);

        EditorGUILayout.EndVertical();



        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Rear right wheel collider", EditorStyles.boldLabel);
        rearRightCollider = (WheelCollider)EditorGUILayout.ObjectField(rearRightCollider, typeof(WheelCollider), true);

        EditorGUILayout.EndVertical();



        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Rear left wheel collider", EditorStyles.boldLabel);
        rearLeftCollider = (WheelCollider)EditorGUILayout.ObjectField(rearLeftCollider, typeof(WheelCollider), true);

        EditorGUILayout.EndVertical();



        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Acceleration", EditorStyles.boldLabel);
        acceleration = EditorGUILayout.FloatField(acceleration);

        EditorGUILayout.EndVertical();



        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Breaking", EditorStyles.boldLabel);
        breaking = EditorGUILayout.FloatField(breaking);

        EditorGUILayout.EndVertical();



        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Speed limit", EditorStyles.boldLabel);
        speedLimit = EditorGUILayout.IntField(speedLimit);

        EditorGUILayout.EndVertical();



        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Check position", EditorStyles.boldLabel);
        check = (Transform)EditorGUILayout.ObjectField(check, typeof(Transform), true);

        EditorGUILayout.EndVertical();


        if (GUILayout.Button("Apply") && !carmodel.GetComponent<CarAIController>())
        {
            CarAIController controller = carmodel.AddComponent<CarAIController>();

            controller.frontRight = frontRight;
            controller.frontLeft = frontLeft;
            controller.rearLeft = rearLeft;
            controller.rearRight = rearRight;

            controller.frontRightCollider = frontRightCollider;
            controller.frontLeftCollider = frontLeftCollider;
            controller.rearRightCollider = rearRightCollider;
            controller.rearLeftCollider = rearLeftCollider;

            controller.acceleration = acceleration;
            controller.breaking = breaking;
            controller.speedLimit = speedLimit;
            controller.check = check;
        }
    }

    void SpawnCheckpoints()
    {
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Checkpoint speed limit", EditorStyles.boldLabel);
        checkpointSpeedLimit = EditorGUILayout.IntField(checkpointSpeedLimit);

        EditorGUILayout.EndVertical();


        checkpointScale = EditorGUILayout.Vector3Field("Checkpoint scale", checkpointScale);




        if (GUILayout.Button("Spawn checkpoint") && checkpointSpeedLimit != 0 && checkpointScale.x != 0 && checkpointScale.y != 0 && checkpointScale.z != 0)
        {
            currentGameObject = Selection.activeGameObject;

            Vector3 pos;

            if (currentGameObject == null || currentGameObject.GetComponent<CheckpointScript>() == false)
            {
                pos = new Vector3(0f, 0f, 0f);
            }
            else
            {
                pos = currentGameObject.transform.position + currentGameObject.transform.forward * 5;
            }

            GameObject checkpoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
            checkpoint.AddComponent<CheckpointScript>();
            checkpoint.name = "Checkpoint";
            checkpoint.transform.localScale = checkpointScale;
            checkpoint.transform.position = pos;
            checkpoint.GetComponent<CheckpointScript>().speedLimit = checkpointSpeedLimit;
            checkpoint.GetComponent<BoxCollider>().isTrigger = true;

            if (currentGameObject != null && currentGameObject.GetComponent<CheckpointScript>())
            {
                checkpoint.transform.forward = currentGameObject.transform.forward;

                CheckpointScript script = currentGameObject.GetComponent<CheckpointScript>();

                script.nextCheckpoints.Add(checkpoint.transform);

                Selection.activeGameObject = checkpoint;


            }


        }
    }

    void CreateIntersection()
    {
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Number of stops", EditorStyles.boldLabel);
        stops = EditorGUILayout.IntField(stops);

        EditorGUILayout.EndVertical();


        if(GUILayout.Button("Spawn intersection") && stops != 0)
        {
            GameObject intersection = new GameObject("Intersection");

            IntersectionScript intersectionScript = intersection.AddComponent<IntersectionScript>();

            for(int i = 1; i <= stops; i++)
            {
                GameObject stop = GameObject.CreatePrimitive(PrimitiveType.Cube);
                stop.name = "Stop" + i.ToString();
                stop.transform.SetParent(intersection.transform);
                stop.GetComponent<BoxCollider>().isTrigger = true;

                StopScript stopScript = stop.AddComponent<StopScript>();
                stopScript.stop = true;

                intersectionScript.stops.Add(stop);

            }

        }


    }
}
