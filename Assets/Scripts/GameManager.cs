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
    [Space(10)]
    public Object PreviousScene;
    public Object NextScene;

    public int MaxTargetSetFails = 5;
    private int CurrentTargetSetFails = 0;

    [SerializeField] private DataStore gameData;

    private bool SceneProgression = false;

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

                // Progress the player to the next scene
                SceneProgression = true;
                StartCoroutine(waiter());
            }
        }
    }

    // Direct scenes forward or backward depending on 'SceneProgression' value when called
    IEnumerator waiter()
    {
        Debug.Log("Countdown to next scene started");

        yield return new WaitForSeconds(3.0f);

        if(SceneProgression == true)
        {
            LoadNextScene();
        }
        else
        {
            LoadPreviousScene();
        }
    }

    void LoadPreviousScene()
    {
        if (PreviousScene != null)
        {
            SceneManager.LoadScene(PreviousScene.name);
        }
    }

    void LoadNextScene()
    {
        if (NextScene != null)
        {
            SceneManager.LoadScene(NextScene.name);
        }
    }

    void RestartCurrentScene()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
