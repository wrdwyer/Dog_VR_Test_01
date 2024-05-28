using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;
using UnityEngine.Splines;
using UnityEngine.Pool;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit;
using HutongGames.PlayMaker.Actions;

namespace DogVR.Actions
    {
    [HelpURL("https://www.youtube.com/watch?v=4h4tiUzqwq8&t=68s")]
    [RequireComponent(typeof(SplineContainer))]
    public class DrawPathToObjective : MonoBehaviour
        {
        [SerializeField]
        [Required("Objective Required")]
        private GameObject Objective = null;
        //[SerializeField]
        //[Required("Player Required")]
        private GameObject Player = null;
        [SerializeField]
        [Required("Controller Input Action Required")]
        public InputActionReference CopperSniffInput = null;

        [SerializeField]
        [Required("Projectile Required")]
        private Projectile ProjectilePrefab;
        [SerializeField]
        private float projectileSpeed = 10f;

        [SerializeField]
        [Required("Line Renderer Required")]
        private LineRenderer Path;
        [SerializeField]
        [Range(0, 10)]
        private float PathHeightOffset = 1.25f;
        [SerializeField]
        [Range(0, 10)]
        private float SpawnHeightOffset = 1.5f;
        [SerializeField]
        [Range(0, 120)]
        private float PathUpdateSpeed = 0.25f;
        private NavMeshTriangulation Triangulation;
        private Vector3[] pathPoints;

        // stack-based ObjectPool available with Unity 2021 and above
        private IObjectPool<Projectile> objectPool;

        // throw an exception if we try to return an existing item, already in the pool
        [SerializeField] private bool collectionCheck = true;

        // extra options to control the pool capacity and maximum size
        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 100;

        private void Awake()
            {                    
            objectPool = new ObjectPool<Projectile>(CreateProjectile,
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize);
            Triangulation = NavMesh.CalculateTriangulation();
            Player = GameObject.FindWithTag("Player");
            ClearSpline();
            }

        private void Update()
            {
            if (Objective != null)
                {
                if (CopperSniffInput != null)
                    {
                    if (CopperSniffInput.action.WasPressedThisFrame())
                        {
                        ClearSpline();  
                        CallDrawNavMechPath();
                        FireProjectile();
                        }
                    }
                }
            }
        
        // invoked when creating an item to populate the object pool
        private Projectile CreateProjectile()
            {
            Projectile projectileInstance = Instantiate(ProjectilePrefab);
            projectileInstance.ObjectPool = objectPool;
            return projectileInstance;
            }

        // invoked when retrieving the next item from the object pool
        private void OnGetFromPool(Projectile pooledObject)
            {
            pooledObject.GetComponent<SplineAnimate>().Container = GetComponent<SplineContainer>();
            pooledObject.transform.position = Player.transform.position;
            pooledObject.GetComponent<SplineAnimate>().Restart(true);
            pooledObject.gameObject.SetActive(true);
            }

        // invoked when returning an item to the object pool
        private void OnReleaseToPool(Projectile pooledObject)
            {
            pooledObject.gameObject.SetActive(false);
            //reset the projectile
            pooledObject.transform.position = Player.transform.position;
            pooledObject.transform.rotation = Quaternion.identity;
            }

        // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
        private void OnDestroyPooledObject(Projectile pooledObject)
            {
            Destroy(pooledObject.gameObject);
            }

        private void Start()
            {

            }

        public void DrawPath()// Use if you want to draw a path on every set number of seconds
            {
            StartCoroutine(DrawPathToCollectable());
            }

        //https://www.youtube.com/watch?v=scaBHHFKLL0
        private IEnumerator DrawPathToCollectable()
            {
            WaitForSeconds Wait = new WaitForSeconds(PathUpdateSpeed);
            NavMeshPath path = new NavMeshPath();

            while (Objective != null)
                {
                if (NavMesh.CalculatePath(Player.transform.position, Objective.transform.position, NavMesh.AllAreas, path))
                    {
                    Path.positionCount = path.corners.Length;
                    for (int i = 0; i < path.corners.Length; i++)
                        {
                        Path.SetPosition(i, path.corners[i] + Vector3.up * PathHeightOffset);
                        }
                    }
                else
                    {
                    Debug.LogError($"Unable to calculate a path on the NavMesh between {Player.transform.position} and {Objective.transform.position}!");
                    }

                yield return Wait;
                }
            }

        [Button("Draw NaveMesh Path")]
        private void CallDrawNavMechPath()
            {
            ClearSpline();
            Vector3 playerPosition = new Vector3(0, 0, 0) + Player.transform.position;
            DrawNaveMeshPath(Objective);
            }

        [Button("Clear Spline")]
        private void ClearSpline()
            {
            if (this.gameObject.TryGetComponent<SplineContainer>(out SplineContainer spline))
                {
                spline.RemoveSplineAt(0);
                spline.Splines = null;
                }
            
            }

        [Button("Fire Projectile")]
        private void FireProjectile()
            {
            // get a pooled object instead of instantiating
            Projectile bulletObject = objectPool.Get();

            if (bulletObject != null)
                {
                StartCoroutine(bulletObject.FireProjectileOnce(bulletObject));
                }
            }

        /*
         * private IEnumerator FireProjectileOnce(Projectile bulletObject)
            {
            while (bulletObject.GetComponent<SplineAnimate>().IsPlaying)
                {
                yield return null;
                }
            Projectile.SetActive(false);
            Projectile.transform.position = Vector3.zero;
            Projectile.transform.rotation = Quaternion.identity;
            yield return new WaitForEndOfFrame();
            }
        */

        public void DrawNaveMeshPath(GameObject Objective)
            {
            Player = GameObject.FindWithTag("Player");
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(Player.transform.position, Objective.transform.position, NavMesh.AllAreas, path))
                {
                Path.positionCount = path.corners.Length;
                for (int i = 0; i < path.corners.Length; i++)
                    {
                    Path.SetPosition(i, path.corners[i] + Vector3.up * PathHeightOffset);
                    }
                }
            else
                {
                Debug.LogError($"Unable to calculate a path on the NavMesh between {Player} and {Objective.transform.position}!");
                }
            CreateSpline(path.corners);
            }

        //https://www.youtube.com/watch?v=4h4tiUzqwq8&t=68s
        public void CreateSpline(Vector3[] pathPoints)
            {
            var container = GetComponent<SplineContainer>();
            var spline = container.AddSpline();
            var knots = new BezierKnot[pathPoints.Length];
            for (int i = 0; i < pathPoints.Length; i++)
                {
                knots[i] = new BezierKnot(Path.GetPosition(i));// + Vector3.up * SpawnHeightOffset
                }
            spline.Knots = knots;
            }
        }
    }
