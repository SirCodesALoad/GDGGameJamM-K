using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Phyisics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Targetable")
        {
            //Debug.Log("Hittable Object Hit!");
            if (col.gameObject.GetComponent<Target>())
            {
                col.gameObject.GetComponent<Target>().OnHit();
                if (col.gameObject.GetComponent<Target>().hit)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                col.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                Destroy(gameObject);
            }
        }
        //Debug.Log("Object Hit: " + col.gameObject.name + " Tag: " + col.gameObject.tag);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
