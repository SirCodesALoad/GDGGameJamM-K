using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Forward, Backward, Left, Right, Up, Down
}
public class TargetThatFiresProjectile : Target
{
    [SerializeField] private Direction DirectionToTravelIn = Direction.Right;
    
    // I've made this a bullet for now. But we can easily make this a Phyisics object, but we're likely still going to end up with relying on the hitscan to reliably trigger the colliders.
    [SerializeField] public GameObject  ObjectToSpawn;
    [SerializeField] public Transform  ProjectileSpawnPoint;

    
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
            FireProjectile();
        }
    }

    public void FireProjectile()
    {
        _collider.enabled = false;
        var direction = Vector3.forward;

        switch(DirectionToTravelIn)
        {
            case Direction.Forward:
                direction = Vector3.forward;
                break;
            case Direction.Backward:
                direction = Vector3.back;
                break;
            case Direction.Left:
                direction = Vector3.left;
                break;
            case Direction.Right:
                direction = Vector3.right;
                break;
            case Direction.Up:
                direction = Vector3.up;
                break;
            case Direction.Down:
                direction = Vector3.down;
                break;
        }

        Debug.Log("PROJECT FIRED");
        GameObject Projectile = Instantiate(ObjectToSpawn, ProjectileSpawnPoint.position, Quaternion.identity);
        Projectile.GetComponent<Bullet>().BulletSpawnPoint = ProjectileSpawnPoint;
        Projectile.transform.LookAt(ProjectileSpawnPoint.position + direction);

    }
    
    public override void Reset()
    {
        _collider.enabled = true;
        hit = false;
    }
    
    void OnDrawGizmosSelected()
    {
        // Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
