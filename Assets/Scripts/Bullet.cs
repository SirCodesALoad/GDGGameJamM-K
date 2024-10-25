using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    private Collider[] CollidersCaughtInExplosion = new Collider[20];
    [SerializeField] private ParticleSystem ExplosionParticleSystem;
    public ParticleSystem ImpactParticleSystem;
    [SerializeField]
    public TrailRenderer BulletTrail;
	[SerializeField]
    public LayerMask Mask;

	[SerializeField]
    public float BulletDistance = 10f, Speed = 100f;
    [SerializeField]
    private float BounceDistance = 10f, ExplosionForceForPhysObjects = 100;
    [SerializeField]
    private int MaxNumberTimesCanBounce = 2, NumberOfTimesBounced = 0, ExplosionRadius = 4;

    public bool BouncingBullets = false, PentratingBullet = false, ExplodingBullet = false;
  
    void Start()
    {
        BulletSpawnPoint = GameObject.Find("Player").GetComponent<PlayerShootScript>().bulletOrigin;
        
        // Should be Object Pooled but we don't expect it will matter in the context of our game.
        Vector3 direction = transform.forward;
        TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

		RaycastHit[] hit = Physics.RaycastAll(BulletSpawnPoint.position, direction, float.MaxValue,
            Mask);
        if (hit.Length > 0)
        {
            var hitPoint = PentratingBullet ? hit[hit.Length - 1] : hit[0];
            StartCoroutine(SpawnTrail(trail, hitPoint.point, hitPoint.normal, BulletDistance * 100, true, hit));
        }
        else
        {
            StartCoroutine(SpawnTrail(trail, BulletSpawnPoint.position + direction * 100, Vector3.zero,
                BulletDistance, false, hit));
        }
    }

    protected IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, float BulletDistance, bool MadeImpact, RaycastHit[] rayCastHit)
    {
        Vector3 startPosition = Trail.transform.position;
        Vector3 direction = (HitPoint - Trail.transform.position).normalized;

        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float startingDistance = distance;

        while (distance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (distance / startingDistance));
            distance -= Time.deltaTime * Speed;

            yield return null;
        }

        Trail.transform.position = HitPoint;
		Destroy(Trail.gameObject, Trail.time);
        if (MadeImpact)
        {
            OnHit(rayCastHit);
            if (BouncingBullets && BounceDistance > 0 && NumberOfTimesBounced <= MaxNumberTimesCanBounce)
            {
                MaxNumberTimesCanBounce++;
                Vector3 bounceDirection = Vector3.Reflect(direction, HitNormal);
                RaycastHit[] hit = Physics.RaycastAll(HitPoint, bounceDirection, BounceDistance, Mask);

                if (hit.Length > 0)
                {
                    yield return StartCoroutine(SpawnTrail(
                        Trail,
                        hit[0].point,
                        hit[0].normal,
                        BounceDistance - Vector3.Distance(hit[0].point, HitPoint),
                        true, hit
                    ));
                }
                else
                {
                    yield return StartCoroutine(SpawnTrail(
                        Trail,
                        HitPoint + bounceDirection * BounceDistance,
                        Vector3.zero,
                        0,
                        false, hit
                    ));
                }
            }
            else if (ExplodingBullet)
            {
                int numColliders = Physics.OverlapSphereNonAlloc(HitPoint, ExplosionRadius, CollidersCaughtInExplosion, Mask);
                var Explosion = Instantiate(ExplosionParticleSystem, HitPoint, Quaternion.identity);
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
                
                Destroy(Explosion, 0.5f);
            }
        }
    }

    public virtual void OnHit(RaycastHit[] rayCastHit)
    {
        for (int i = 0; i < rayCastHit.Length; i++)
        {
            var hit = rayCastHit[i];
            if (hit.collider.gameObject.tag == "Targetable")
            {
                //Debug.Log("Hittable Object Hit!");
                if (hit.collider.gameObject.GetComponent<Target>())
                {
                    hit.collider.gameObject.GetComponent<Target>().OnHit();
                }
            }

            if (!PentratingBullet)
            {
                return;
            }
        }
    }
}
