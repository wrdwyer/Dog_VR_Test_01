using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Splines;

public class Projectile : MonoBehaviour
    {
    // deactivate after delay
    [SerializeField] private float timeoutDelay = 5f;

    private IObjectPool<Projectile> objectPool;

    // public property to give the projectile a reference to its ObjectPool
    public IObjectPool<Projectile> ObjectPool { set => objectPool = value; }

    public IEnumerator FireProjectileOnce(Projectile bulletObject)
        {
        bulletObject.GetComponent<SplineAnimate>().Play();
        if (bulletObject.GetComponent<ParticleSystem>() != null)
            {
            bulletObject.GetComponent<ParticleSystem>().Play();
            }

        while (bulletObject.GetComponent<SplineAnimate>().IsPlaying)
            {
            yield return null;
            }
        /*if (bulletObject.GetComponent<ParticleSystem>() != null)
            {
            bulletObject.GetComponent<ParticleSystem>().Stop();
            }
        */
        // release the projectile back to the pool
        yield return new WaitForSeconds(5f);
        bulletObject.GetComponent<ParticleSystem>().Stop(); 
        
        objectPool.Release(bulletObject);

        
        }
    }

