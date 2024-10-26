using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public enum AmmoTypes
{
    Bullet,
    Riochet,
    Pen,
    Explode
}

public class PlayerShootScript : MonoBehaviour
{
    public Transform bulletOrigin;
    public int currentAmmo, maxAmmo = 20, riochetAmmo = 0, penAmmo = 0, explodeAmmo = 0;
	[SerializeField]
    private ParticleSystem MuzzleFlash;
    [SerializeField] public GameObject  Bullet;
    public AmmoTypes ActiveAmmo;

    [SerializeField] private DataStore gameData;

    //public float  bulletForce = 10000f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        currentAmmo = maxAmmo;
        ActiveAmmo = AmmoTypes.Bullet;

        // Get new or persistent ammo values
        if(gameData.NewRun == true)
        {
            currentAmmo = gameData.DefaultRegAmmo;
            riochetAmmo = gameData.DefaultRicAmmo;
            penAmmo = gameData.DefaultPenAmmo;
            explodeAmmo = gameData.DefaultExpAmmo;
            gameData.NewRun = false;
            gameData.Reset();
        }
        else
        {
            currentAmmo = gameData.CurrentRegAmmo;
            riochetAmmo = gameData.CurrentRicAmmo;
            penAmmo = gameData.CurrentPenAmmo;
            explodeAmmo = gameData.CurrentExpAmmo;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenPointToRay(mouseScreenPos).GetPoint(200f);
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
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Ammo Switched to: Explode");
            ActiveAmmo = AmmoTypes.Explode;
        }

        gameData.GameTime += Time.deltaTime;
    }

    void Fire(Vector3 mouseWorldPos)
    {
        if (currentAmmo > 0)
        {
            //currentAmmo--;
            gameData.TotalShotsFired++;
            var bulletInstance = Instantiate(Bullet, bulletOrigin.transform.position, Quaternion.identity);
            var bullet = bulletInstance.GetComponent<Bullet>();
            bullet.BulletSpawnPoint = bulletOrigin;
            switch(ActiveAmmo)
            {
                case AmmoTypes.Bullet:
                    bullet.BouncingBullets = false;
                    bullet.PentratingBullet = false;
                    bullet.ExplodingBullet = false;
                    currentAmmo--;
                    gameData.RegShotsFired++;
                    gameData.CurrentRegAmmo = currentAmmo;
                    break;
                case AmmoTypes.Riochet:
                    bullet.BouncingBullets = true;
                    bullet.PentratingBullet = false;
                    bullet.ExplodingBullet = false;
                    riochetAmmo--;
                    gameData.RicShotsFired++;
                    gameData.CurrentRicAmmo = riochetAmmo;
                    break;
                case AmmoTypes.Pen:
                    bullet.BouncingBullets = false;
                    bullet.PentratingBullet = true;
                    bullet.ExplodingBullet = false;
                    penAmmo--;
                    gameData.PenShotsFired++;
                    gameData.CurrentPenAmmo = penAmmo;
                    break;
                case AmmoTypes.Explode:
                    bullet.BouncingBullets = false;
                    bullet.PentratingBullet = false;
                    bullet.ExplodingBullet = true;
                    explodeAmmo--;
                    gameData.ExpShotsFired++;
                    gameData.CurrentExpAmmo = explodeAmmo;
                    break;
            }
                
            bulletOrigin.transform.LookAt(mouseWorldPos);
            bulletInstance.transform.LookAt(mouseWorldPos);
        }
    }
}
