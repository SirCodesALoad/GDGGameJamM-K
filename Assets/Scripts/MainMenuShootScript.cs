using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;
using UnityEngine.EventSystems;
public class MainMenuShootScript : MonoBehaviour
{
    public Transform bulletOrigin;
    private ParticleSystem MuzzleFlash;
    [SerializeField] public GameObject  Bullet;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenPointToRay(mouseScreenPos).GetPoint(200f);
        bulletOrigin.transform.LookAt(mouseWorldPos);
        if (Input.GetMouseButtonDown(0))
        {
			//MuzzleFlash.Play();
            Fire(mouseWorldPos);
        }
    }


    void Fire(Vector3 mouseWorldPos)
    {
        var bulletInstance = Instantiate(Bullet, bulletOrigin.transform.position, Quaternion.identity);
        var bullet = bulletInstance.GetComponent<Bullet>();
        bullet.BulletSpawnPoint = bulletOrigin;
        bulletOrigin.transform.LookAt(mouseWorldPos);
        bulletInstance.transform.LookAt(mouseWorldPos);
    }
}
