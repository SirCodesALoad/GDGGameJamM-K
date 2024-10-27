using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using UnityEngine.EventSystems;

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

    public UnityEvent AmmoChangedEvent;

    public AmmoCounterUI[] AmmoCounterUis = new AmmoCounterUI[]{};
    
    private ActiveBulletLoadedUI CurrentBulletLoaded;

    private UnityEvent GameFailure;

    public AudioClip RegGunShot;
    public AudioClip PenGunShot;
    public AudioClip RicGunShot;
    public AudioClip ExpGunShot;

    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Confined;
        currentAmmo = maxAmmo;
        CurrentBulletLoaded = GameObject.Find("CurrentBulletLoaded").GetComponent<ActiveBulletLoadedUI>();
        if (GameFailure == null)
        {
            new UnityEvent();
        }

        // Get new or persistent ammo values
        if(gameData.NewRun == true)
        {
            gameData.Reset();
            currentAmmo = gameData.DefaultRegAmmo;
            riochetAmmo = gameData.DefaultRicAmmo;
            penAmmo = gameData.DefaultPenAmmo;
            explodeAmmo = gameData.DefaultExpAmmo;
            gameData.CurrentRegAmmo =  gameData.DefaultRegAmmo;
            gameData.CurrentRicAmmo = gameData.DefaultRicAmmo;
            gameData.CurrentPenAmmo = gameData.DefaultPenAmmo;
            gameData.CurrentExpAmmo = gameData.DefaultExpAmmo;
            gameData.NewRun = false;
        }
        else
        {
            currentAmmo = gameData.CurrentRegAmmo;
            riochetAmmo = gameData.CurrentRicAmmo;
            penAmmo = gameData.CurrentPenAmmo;
            explodeAmmo = gameData.CurrentExpAmmo;
        }
        
        Debug.Log(riochetAmmo);
        Debug.Log(penAmmo);
        Debug.Log(explodeAmmo);

        if (AmmoChangedEvent == null)
        {
            AmmoChangedEvent = new UnityEvent();
        }

        if (AmmoCounterUis[0] == null)
        {
            var GameObjects = GameObject.FindGameObjectsWithTag("BulletDisplay");
            for (int i = 0; i < GameObjects.Length; i++)
            {
                AmmoCounterUis[i] = GameObjects[i].GetComponent<AmmoCounterUI>();
            }
        }
        for (int i = 0; i < AmmoCounterUis.Length; i++)
        {
            AmmoChangedEvent.AddListener(AmmoCounterUis[i].UpdateAmmo);
        }

        PlayerSwapActiveAmmo(AmmoTypes.Bullet);
        this.Invoke("InvokeAmmoChangedEvent", 0.2f);
    }

    void InvokeAmmoChangedEvent()
    {
        AmmoChangedEvent.Invoke();
    }

    public void PlayerSwapActiveAmmo(AmmoTypes ammo)
    {
        ActiveAmmo = ammo;
        CurrentBulletLoaded.DisplayCurrentActiveAmmo(ammo);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenPointToRay(mouseScreenPos).GetPoint(55f);
        bulletOrigin.transform.LookAt(mouseWorldPos);
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && CheckIfCanFire())
        {
			//MuzzleFlash.Play();
            Fire(mouseWorldPos);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        gameData.GameTime += Time.deltaTime;
    }

    public bool CheckIfCanFire()
    {
        switch(ActiveAmmo)
        {
            case AmmoTypes.Bullet:
                if (currentAmmo > 0)
                {
                    return true;
                }
                break;
            case AmmoTypes.Riochet:
                if (riochetAmmo > 0)
                {
                    return true;
                }
                break;
            case AmmoTypes.Pen:
                if (penAmmo > 0)
                {
                    return true;
                }
                break;
            case AmmoTypes.Explode:
                if (explodeAmmo > 0)
                {
                    return true;
                }
                break;
        }
        if (currentAmmo <= 0 && explodeAmmo  <= 0 && penAmmo <= 0 && riochetAmmo <= 0)
        {
            //We've run out of ammo game failure.
            GameFailure.AddListener(GameObject.Find("GameManager").GetComponent<GameManager>().OnGameLost);
            GameFailure.Invoke();
            return false;
        }
        return false;
    }

    void Fire(Vector3 mouseWorldPos)
    {
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
        AmmoChangedEvent.Invoke();
        bulletOrigin.transform.LookAt(mouseWorldPos);
        bulletInstance.transform.LookAt(mouseWorldPos);
            
        PlayerSwapActiveAmmo(AmmoTypes.Bullet);
    }
}
