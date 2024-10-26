using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public enum BackBoardMaterial
{
    Wood,
    Metal
}

public class Target : MonoBehaviour
{
    public BackBoardMaterial backBoard = BackBoardMaterial.Wood;
    public bool hit = false;
    private static GameManager Manager;
    public UnityEvent<Target> TargetHit; 
	public bool ReportHitToGameManager = true;
    protected Collider _collider;
    
    void Start()
    {
        if (TargetHit == null)
            TargetHit = new UnityEvent<Target>();

        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        TargetHit.AddListener(Manager.PlayerHitTarget);

        _collider = this.GetComponent<Collider>();
    }

    public virtual void OnHit()
    {
        if (!hit)
        {
            hit = true;
            StopAllCoroutines();
            StartCoroutine(KnockDown());
			if (ReportHitToGameManager)
			{
            	TargetHit.Invoke(this);
			}
        }
    }

    IEnumerator KnockDown()
    {
        Quaternion currentRot = transform.rotation;
        float counter = 0f;
        while (counter < 0.5 && hit)
        {
            _collider.enabled = false;
            counter += Time.deltaTime;
            
            transform.rotation = Quaternion.Lerp(currentRot, Quaternion.Euler(new Vector3(90f, 0f, 0f)), counter / 0.5f);
            yield return null;
        }
    }
     
    IEnumerator ResetTarget()
    {
        Quaternion currentRot = transform.rotation;

        float counter = 0f;
        while (counter < 0.5f)
        {
            hit = false;
            _collider.enabled = true;
            counter += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(currentRot, Quaternion.Euler(new Vector3(0f, 0f, 0f)), counter / 0.5f);
            yield return null;
        }
    }
    
    public virtual void Reset()
    {
        hit = false;
        _collider.enabled = true;
        Debug.Log("ResetTarget");
        StopAllCoroutines();
        StartCoroutine(ResetTarget());
    }
}
