using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "DataStorage/DataStore")]
public class DataStore : ScriptableObject
{
    public bool NewRun = true;

    public int DefaultRegAmmo = 200;
    public int DefaultPenAmmo = 10;
    public int DefaultRicAmmo = 10;
    public int DefaultExpAmmo = 10;

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

    public float GameTime = 0.0f;

    // Called at the start of a run from PlayerShootScript
    public void Reset()
    {
        CurrentRegAmmo = 0;
        CurrentPenAmmo = 0;
        CurrentRicAmmo = 0;
        CurrentExpAmmo = 0;

        RegShotsFired = 0;
        PenShotsFired = 0;
        RicShotsFired = 0;
        ExpShotsFired = 0;

        TotalShotsFired = 0;

        TargetSetsPassed = 0;
        TargetSetsFailed = 0;

        LevelProgressions = 0;
        LevelRegressions = 0;

        GameTime = 0.0f;
    }

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
