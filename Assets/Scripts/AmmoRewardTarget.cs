using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRewardTarget : Target
{

    [SerializeField] private AmmoTypes AmmoToAward;
    [SerializeField] private int AmountOfAmmoToAward = 1;
    void Start()
    {
        ReportHitToGameManager = false;
    }

    public override void OnHit()
    {
        if (!hit)
        {
            hit = true;
            AwardBulletToPlayer();
        }
    }

    private void AwardBulletToPlayer()
    {
        gameObject.SetActive(false);
        switch(AmmoToAward)
        {
            case AmmoTypes.Bullet:
                GameObject.Find("Player").GetComponent<PlayerShootScript>().currentAmmo += AmountOfAmmoToAward;
                break;
            case AmmoTypes.Riochet:
                GameObject.Find("Player").GetComponent<PlayerShootScript>().riochetAmmo += AmountOfAmmoToAward;
                break;
            case AmmoTypes.Pen:
                GameObject.Find("Player").GetComponent<PlayerShootScript>().penAmmo += AmountOfAmmoToAward;
                break;
            case AmmoTypes.Explode:
                GameObject.Find("Player").GetComponent<PlayerShootScript>().explodeAmmo += AmountOfAmmoToAward;
                break;
        }
    }

    public override void Reset()
    {
        hit = false;
    }
}
