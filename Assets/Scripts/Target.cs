using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BackBoardMaterial
{
    Wood,
    Metal
}

public class Target : MonoBehaviour
{
    public BackBoardMaterial backBoard = BackBoardMaterial.Wood;
    private bool hit = false;

    public virtual void OnHit()
    {
        if (!hit)
        {
            //transform.Rotate(Vector3.right, -90f);
            StartCoroutine(KnockDown());
        }
        hit = true;
    }

    IEnumerator KnockDown()
    {
        Quaternion currentRot = transform.rotation;

        float counter = 0f;
        while (counter < 0.5)
        {
            counter += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(currentRot, Quaternion.Euler(new Vector3(90f, 0f, 0f)), counter / 0.5f);
            yield return null;
        }
    }
    
    public virtual void Reset()
    {
        transform.Rotate(Vector3.right, 0f);
        hit = false;
    }
}
