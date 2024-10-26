using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActiveBulletLoadedUI : MonoBehaviour
{
    public GameObject BulletUiObject, RiochetUiObject, PenUiObject, ExplosiveUiObject;
    public void DisplayCurrentActiveAmmo(AmmoTypes ammo)
    {
        switch(ammo)
        {
            case AmmoTypes.Bullet:
                BulletUiObject.SetActive(true);
                RiochetUiObject.SetActive(false);
                PenUiObject.SetActive(false);
                ExplosiveUiObject.SetActive(false);
                break;
            case AmmoTypes.Riochet:
                BulletUiObject.SetActive(false);
                RiochetUiObject.SetActive(true);
                PenUiObject.SetActive(false);
                ExplosiveUiObject.SetActive(false);
                break;
            case AmmoTypes.Pen:
                BulletUiObject.SetActive(false);
                RiochetUiObject.SetActive(false);
                PenUiObject.SetActive(true);
                ExplosiveUiObject.SetActive(false);
                break;
            case AmmoTypes.Explode:
                BulletUiObject.SetActive(false);
                RiochetUiObject.SetActive(false);
                PenUiObject.SetActive(false);
                ExplosiveUiObject.SetActive(true);
                break;
        }
    }
}
