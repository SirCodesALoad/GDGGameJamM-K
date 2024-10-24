// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class RiochetBullet : Bullet
// {
//     [SerializeField]
//     private float BounceDistance = 10f;
//     [SerializeField]
//     private int MaxNumberTimesCanBounce = 2, NumberOfTimesBounced = 0;
//     
//     public override void OnHit(RaycastHit Hit, TrailRenderer Trail)
//     {
//         if (Hit.collider.gameObject.tag == "Targetable")
//         {
//             if (Hit.collider.gameObject.GetComponent<Target>())
//             {
//                 var TargetHit = Hit.collider.gameObject.GetComponent<Target>();
//                 TargetHit.OnHit();
//
//                 Vector3 startPosition = Trail.transform.position;
//                 Vector3 direction = (Hit.point - Trail.transform.position).normalized;
//
//                 float distance = Vector3.Distance(Trail.transform.position, Hit.point);
//                 
//                 //Instantiate(ImpactParticleSystem, Hit.point, Quaternion.LookRotation(Hit.normal));
//
//                 if (NumberOfTimesBounced <= MaxNumberTimesCanBounce && TargetHit.backBoard == BackBoardMaterial.Metal )
//                 {
//                     MaxNumberTimesCanBounce++;
//                     Vector3 bounceDirection = Vector3.Reflect(direction, Hit.normal);
//
//                     if (Physics.Raycast(Hit.point, bounceDirection, out RaycastHit hit, BounceDistance, Mask))
//                     {
//                         StartCoroutine(StartRiochet(
//                             hit.point,
//                             hit.normal,
//                             BounceDistance - Vector3.Distance(hit.point, Hit.point),
//                             true,
//                             hit
//                         ));
//                     }
//                     else
//                     {
//                         StartCoroutine(StartRiochet(
//                             Hit.point + bounceDirection * BounceDistance,
//                             Vector3.zero,
//                             0,
//                             false,
//                             hit
//                         ));
//                     }
//                 }
//                 else
//                 {
//                     Destroy(this);
//                 }
//             }
//             else
//             {
//                 Destroy(this);
//             }
//         }
//         else
//         {
//             Destroy(this);
//         }
//     }
//
//     protected IEnumerator StartRiochet(Vector3 HitPoint, Vector3 HitNormal, float BulletDistance,
//         bool MadeImpact, RaycastHit hit)
//     {
//         TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
//         yield return StartCoroutine(SpawnTrail(
//             trail,
//             HitPoint,
//             HitNormal,
//             BulletDistance,
//             MadeImpact,
//             hit
//         ));
//     }
//     
// }