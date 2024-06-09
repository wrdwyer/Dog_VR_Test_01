using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Splines;

namespace DogVR.Actions
    {

    public class SniffProjectile : MonoBehaviour
        {
        // deactivate after delay
        [SerializeField] private float timeoutDelay = 5f;

        //private IObjectPool<Projectile> objectPool;

        // public property to give the projectile a reference to its ObjectPool
        //public IObjectPool<Projectile> ObjectPool { set => objectPool = value; }

        public IEnumerator FireSnifffOnce()
            {
            SplineAnimate splineAnimate = GetComponent<SplineAnimate>();
            ParticleSystem particleSystem = GetComponent<ParticleSystem>();
            splineAnimate.Play();
            if (particleSystem != null)
                {
                particleSystem.Play();
                }

            while (splineAnimate.IsPlaying)
                {
                yield return null;
                }
            /*if (bulletObject.GetComponent<ParticleSystem>() != null)
                {
                bulletObject.GetComponent<ParticleSystem>().Stop();
                }
            */
            // release the projectile back to the pool
            yield return new WaitForSeconds(timeoutDelay);

            }
        }
    }

