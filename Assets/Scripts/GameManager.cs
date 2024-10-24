using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public List<Target> TargetSequence = new List<Target>{}, TargetsHit = new List<Target>{};
    public int CurrentTargetIndex;

    public void PlayerHitTarget(Target target)
    {
        Debug.Log("PLAYER HAS HIT TARGET!");
        if (TargetSequence.Count <= CurrentTargetIndex)
        {
            return;
        }
        TargetsHit.Add(target);
        if (target != TargetSequence[CurrentTargetIndex])
        {
            // Oh ho! Player fucked up!
            CurrentTargetIndex = 0;

            for (int i = 0; i < TargetsHit.Count; i++)
            {
                TargetsHit[i].Reset();
            }
            TargetsHit.Clear();
            Debug.Log("PLAYER HAS HIT INCORRECT TARGET!");
        }
        else
        {
            Debug.Log("PLAY HIT CORRECT TARGET");

            CurrentTargetIndex++;
            // Yay! Player is Smort. Play a win sound or something! Give them a cupcake!
            if (CurrentTargetIndex >= TargetSequence.Count)
            {
                //Win! Move onto next stage.
                Debug.Log("PLAYER HAS HIT ALL TARGETS IN SEQUENCE!");
            }
        }
    }

    void RestartCurrentScene()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
