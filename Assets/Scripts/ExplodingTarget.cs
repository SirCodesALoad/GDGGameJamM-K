using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingTarget : Target
{
    private Collider[] CollidersCaughtInExplosion = new Collider[20];
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private int ExplosionRadius = 4;
    [SerializeField] private float ExplosionForceForPhysObjects = 100;
    
    [SerializeField] private ParticleSystem ExplosionParticleSystem;

    void Start()
    {
        _collider = this.GetComponent<Collider>();
        ReportHitToGameManager = false;
    }

    public override void OnHit()
    {
        if (!hit)
        {
            hit = true;
            Explode();
        }
    }

    private void Explode()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, ExplosionRadius, CollidersCaughtInExplosion, LayerMask);
        var Explosion = Instantiate(ExplosionParticleSystem, transform.position, Quaternion.identity);
        Explosion.Play();

        if (numColliders > 0)
        {
            for (int i = 0; i < numColliders; i++)
            {
                if (CollidersCaughtInExplosion[i].TryGetComponent(out Rigidbody rb))
                {
                    rb.AddExplosionForce(ExplosionForceForPhysObjects, transform.position, ExplosionRadius);
                }
                if (CollidersCaughtInExplosion[i].TryGetComponent(out Target target))
                {
                    target.OnHit();
                }
            }
        }
        gameObject.SetActive(false);
        Destroy(Explosion, 1.5f);
        Destroy(this, 2f);
        
    }
    public override void Reset()
    {
        hit = false;
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
