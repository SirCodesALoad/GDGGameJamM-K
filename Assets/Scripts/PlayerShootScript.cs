using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public enum AmmoTypes
{
    Bullet,
    Riochet,
    Pen
}

public class PlayerShootScript : MonoBehaviour
{
    public Transform bulletOrigin;
    public int currentAmmo, maxAmmo = 20;
	[SerializeField]
    private ParticleSystem MuzzleFlash;
    [SerializeField] public GameObject  Bullet;
    public AmmoTypes ActiveAmmo;
    
    
    //public float  bulletForce = 10000f;
    
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        ActiveAmmo = AmmoTypes.Bullet;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos += Camera.main.transform.forward * 200f; 
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        if (Input.GetMouseButtonDown(0))
        {
			//MuzzleFlash.Play();
            Fire(mouseWorldPos);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Ammo Switched to: Riochet");
            ActiveAmmo = AmmoTypes.Riochet;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Ammo Switched to: Bullet");
            ActiveAmmo = AmmoTypes.Bullet;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Ammo Switched to: Pen");
            ActiveAmmo = AmmoTypes.Pen;
        }
    }

    void Fire(Vector3 mouseWorldPos)
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            var bulletInstance = Instantiate(Bullet, bulletOrigin.transform.position, Quaternion.identity);
            var bullet = bulletInstance.GetComponent<Bullet>();
            switch(ActiveAmmo)
            {
                case AmmoTypes.Bullet:
                    bullet.BouncingBullets = false;
                    bullet.PentratingBullet = false;
                    break;
                case AmmoTypes.Riochet:
                    bullet.BouncingBullets = true;
                    bullet.PentratingBullet = false;
                    break;
                case AmmoTypes.Pen:
                    bullet.BouncingBullets = false;
                    bullet.PentratingBullet = true;
                    break;
            }
                
            bulletOrigin.transform.LookAt(mouseWorldPos);
            bulletInstance.transform.LookAt(mouseWorldPos);
        }
    }
}
