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
using Sirenix.Utilities;
using Unity.XR.CoreUtils;
using FMODUnity;

namespace DogVR.Actions
    {
    [HelpURL("https://www.youtube.com/watch?v=4h4tiUzqwq8&t=68s")]
    [RequireComponent(typeof(SplineContainer))]
    public class DrawPathToObjective : MonoBehaviour
        {
        [SerializeField]
        private GameObject Objective = null;
        [SerializeField]
        private GameObject player = null;
        [SerializeField]
        [Required("Controller Input Action Required")]
        public InputActionReference CopperSniffInput = null;
        [SerializeField]
        [Required("Projectile Required")]
        private Projectile ProjectilePrefab;
        [SerializeField]
        private GameObject ScentPrefab;
        [SerializeField]
        private float projectileSpeed = 10f;
        [SerializeField]
        [Required("Line Renderer Required")]
        private LineRenderer Path;
        [SerializeField]
        [Range(-5, 10)]
        private float PathHeightOffset = 1.25f;
        [SerializeField]
        [Range(-5, 10)]
        private float SpawnHeightOffset = 1.5f;
        [SerializeField]
        [Range(0, 120)]
        private float PathUpdateSpeed = 0.25f;
        [SerializeField]
        private float cooldownTime = 3.0f;
        [SerializeField]
        private StudioEventEmitter sniffSound = null;
        private NavMeshTriangulation Triangulation;
        private Vector3[] pathPoints;
        private bool hasFired = false;

        // stack-based ObjectPool available with Unity 2021 and above
        private IObjectPool<Projectile> objectPool;

        // throw an exception if we try to return an existing item, already in the pool
        [SerializeField] private bool collectionCheck = true;

        // extra options to control the pool capacity and maximum size
        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 100;

        private void Awake()
            {

            objectPool = new UnityEngine.Pool.ObjectPool<Projectile>(CreateProjectile,
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize);
            Triangulation = NavMesh.CalculateTriangulation();
            sniffSound = GetComponent<StudioEventEmitter>();
            ClearSpline();
            }
        private void Start()
            {
            var xrOrigin = GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<XROrigin>();
            player = xrOrigin.gameObject;
            //player = GameManager.Instance.playerGameObjectSO.persistentObject;
            //player = GameObject.FindWithTag("Player");
            }

        private void Update()
            {
            if (Objective != null)
                {
                if (CopperSniffInput != null)
                    {
                    if (hasFired == false)
                        {
                        if (CopperSniffInput.action.WasPressedThisFrame())
                            {
                            hasFired = true;
                            if (sniffSound != null)
                                {
                                Debug.Log("Play Sniff Sound");
                                sniffSound.Play();
                                }
                            StartCooldown();
                            //CallDrawNavMechPath();
                            DrawNaveMeshPath(Objective, player);
                            FireProjectile();
                            //FireScent();

                            }
                        }
                    }
                }
            }
        [Button("Get Objective")]
        public void UpdateCurrentObjective()
            {
            Objective = GameManager.Instance.currentObjectiveSO.CurrentObjective;
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
            pooledObject.transform.position = player.transform.position;
            pooledObject.GetComponent<SplineAnimate>().Restart(true);
            pooledObject.gameObject.SetActive(true);
            }

        // invoked when returning an item to the object pool
        private void OnReleaseToPool(Projectile pooledObject)
            {
            pooledObject.gameObject.SetActive(false);
            //reset the projectile
            pooledObject.transform.position = player.transform.position;
            pooledObject.transform.rotation = Quaternion.identity;
            }

        // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
        private void OnDestroyPooledObject(Projectile pooledObject)
            {
            Destroy(pooledObject.gameObject);
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
                if (NavMesh.CalculatePath(player.transform.position, Objective.transform.position, NavMesh.AllAreas, path))
                    {
                    Path.positionCount = path.corners.Length;
                    for (int i = 0; i < path.corners.Length; i++)
                        {
                        Path.SetPosition(i, path.corners[i] + Vector3.up * PathHeightOffset);
                        }
                    }
                else
                    {
                    Debug.LogError($"Unable to calculate a path on the NavMesh between {player.transform.position} and {Objective.transform.position}!");
                    }

                yield return Wait;
                }
            }

        [Button("Draw NaveMesh Path")]
        private void CallDrawNavMechPath()
            {
            ClearSpline();
            //Player = GameObject.FindWithTag("Player");
            //Vector3 playerPosition = new Vector3(0, 0, 0) + Player.transform.position;
            DrawNaveMeshPath(Objective, player);
            }

        [Button("Clear Spline")]
        private void ClearSpline()
            {
            if (this.gameObject.TryGetComponent<SplineContainer>(out SplineContainer spline))
                {
                spline.RemoveSplineAt(0);
                spline.Splines = null;
                //Player = null;
                Debug.Log("Cleared Spline");
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
                Debug.Log("Fired Projectile");
                ClearSpline();
                }
            }
        [Button("Fire Scent")]
        private void FireScent()
            {
            if (ScentPrefab != null)
                {
                ScentPrefab.transform.position = player.transform.position;
                ScentPrefab.SetActive(true);
                //SniffProjectile sniffProjectile = ScentPrefab.GetComponent<SniffProjectile>();
                //StartCoroutine(sniffProjectile.FireSnifffOnce());
                StartCooldown();
                ScentPrefab.SetActive(false);
                Debug.Log("Fired Scent");
                }
            }

        public void StartCooldown()
            {
            hasFired = true;
            if (hasFired)
                {
                Debug.Log("Start Cooldown");
                StartCoroutine(Cooldown());
                }
            }

        private IEnumerator Cooldown()
            {
            yield return new WaitForSeconds(cooldownTime);
            hasFired = false;
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

        public void DrawNaveMeshPath(GameObject Objective, GameObject Player)
            {
            //Player = null;
            //Player = GameObject.FindWithTag("Player");
            if (Player != null)
                {
                Vector3 playerPosition = new Vector3(0, 0, 0) + Player.transform.position;
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(playerPosition, Objective.transform.position, NavMesh.AllAreas, path))
                    {
                    Path.positionCount = path.corners.Length;
                    for (int i = 0; i < path.corners.Length; i++)
                        {
                        Path.SetPosition(i, path.corners[i] + Vector3.up * PathHeightOffset);
                        Debug.Log(path.corners[i] + Vector3.up * PathHeightOffset);
                        }
                    }
                else
                    {
                    Debug.LogError($"Unable to calculate a path on the NavMesh between {Player} and {Objective.transform.position}!");
                    }
                CreateSpline(path.corners);
                }
            else
                {
                Debug.LogError("Player not found");
                }
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
