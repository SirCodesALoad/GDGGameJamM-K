using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounterUI : MonoBehaviour
{
    [SerializeField] private DataStore gameData;
    
    public AmmoTypes AmmoTracking;

    public TextMeshProUGUI AmmoDisplay;


    // Start is called before the first frame update
    void Start()
    {
        AmmoDisplay = transform.GetComponent<TextMeshProUGUI>();
        UpdateAmmo();
    }

    public void UpdateAmmo()
    {
        switch(AmmoTracking)
        {
            case AmmoTypes.Bullet:
                AmmoDisplay.text = gameData.CurrentRegAmmo.ToString();
                break;
            case AmmoTypes.Riochet:
                AmmoDisplay.text = gameData.CurrentRicAmmo.ToString();
                break;
            case AmmoTypes.Pen:
                AmmoDisplay.text = gameData.CurrentPenAmmo.ToString();
                break;
            case AmmoTypes.Explode:
                AmmoDisplay.text = gameData.CurrentExpAmmo.ToString();
                break;
        }
    }

}
