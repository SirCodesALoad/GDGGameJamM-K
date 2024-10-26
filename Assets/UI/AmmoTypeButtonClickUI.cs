using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoTypeButtonClickUI : MonoBehaviour
{
    public AmmoTypes AmmoTypeToSelect;
    private PlayerShootScript Player;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerShootScript>();
    }

    public void ButtonPressed()
    {
        Player.PlayerSwapActiveAmmo(AmmoTypeToSelect);
    }
}
