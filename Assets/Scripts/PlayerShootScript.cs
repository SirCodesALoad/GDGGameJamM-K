using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{
    public Transform bulletOrigin;
    public int currentAmmo, maxAmmo = 20;
    [SerializeField] public Rigidbody bullet;
    public float  bulletForce = 10000f;
    
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos += Camera.main.transform.forward * 200f; 
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        if (Input.GetMouseButtonDown(0))
        {
            Fire(mouseWorldPos);
        }
    }

    void Fire(Vector3 mouseWorldPos)
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            Rigidbody bulletInstance;
            bulletOrigin.transform.LookAt(mouseWorldPos);
            bulletInstance = Instantiate(bullet, bulletOrigin.transform.position, Quaternion.identity) as Rigidbody;
            bulletInstance.transform.LookAt(mouseWorldPos);
            Vector3 bulletDirection = mouseWorldPos - bulletOrigin.transform.position;
            bulletDirection.y += 0.5f;

            bulletInstance.AddForce( bulletDirection * bulletForce);
        }
    }
}
