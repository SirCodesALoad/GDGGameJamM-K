using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "DataStorage/DataStore")]
public class DataStore : ScriptableObject
{
    // Reg = regular ammo / Pen = Penetration ammo / Ric = Ricochet ammo / Exp = Exploding ammo
    public int CurrentRegAmmo = 0;
    public int CurrentPenAmmo = 0;
    public int CurrentRicAmmo = 0;
    public int CurrentExpAmmo = 0;

    public int RegShotsFired = 0;
    public int PenShotsFired = 0;
    public int RicShotsFired = 0;
    public int ExpShotsFired = 0;

    public int TotalShotsFired = 0;

    public int TargetSetsPassed = 0;
    public int TargetSetsFailed = 0;

    public int LevelProgressions = 0;
    public int LevelRegressions = 0;
}

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
