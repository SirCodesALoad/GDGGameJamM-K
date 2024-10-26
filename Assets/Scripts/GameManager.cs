using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public List<Target> TargetSequence = new List<Target>{}, TargetsHit = new List<Target>{};
    public int MaxNumberOfResets = 5, CurrentNumberOfResetsTriggered = 0;
    public float DelayBetweenLevels = 0.5f;
    private int CurrentTargetIndex, CurrentLeveLIndex, MainMenuIndex = 0, GameStartIndex = 1, GameWomIndex = 17, GameLostIndex = 18;

    [SerializeField] private DataStore gameData;

    public float SceneProgressionSoundDelay = 0;
    public float TargetResetSoundDelay = 0;

    public AudioClip SceneProgressionSound;
    public AudioClip SceneRegressionSound;
    public AudioClip TargetResetSound;
    public AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

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
            gameData.TargetSetsFailed++;

            for (int i = 0; i < TargetsHit.Count; i++)
            {
                TargetsHit[i].Reset();
            }
            TargetsHit.Clear();
            Debug.Log("PLAYER HAS HIT INCORRECT TARGET!");
            CurrentNumberOfResetsTriggered++;
            //GetComponent<AudioSource>().clip = TargetResetSound;
            //GetComponent<AudioSource>().PlayDelayed(TargetResetSoundDelay);
            OnPlayerResetTargets();
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
                StartCoroutine(EndLevel());
            }
        }
    }

    void OnPlayerResetTargets()
    {
        if (CurrentNumberOfResetsTriggered >= MaxNumberOfResets)
        {
            gameData.LevelRegressions++;
            GetComponent<AudioSource>().clip = SceneRegressionSound;
            GetComponent<AudioSource>().Play(0);
            var buildIndex = SceneManager.GetActiveScene().buildIndex - 1;
            if (buildIndex > 0)
            {
                SceneManager.LoadScene(buildIndex);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    IEnumerator EndLevel()
    {
        gameData.LevelProgressions++;
        gameData.TargetSetsPassed++;
        GetComponent<AudioSource>().clip = SceneProgressionSound;
        GetComponent<AudioSource>().PlayDelayed(SceneProgressionSoundDelay);
        yield return new WaitForSeconds(DelayBetweenLevels);
        var buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (buildIndex < GameWomIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }
        else
        {
            //Player won!
            Debug.Log("PLAYER HAS COMPLETED ALL SCENES");
            SceneManager.LoadScene(GameWomIndex);
        }
    }

    void RestartCurrentScene()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void StartNewGame()
    {
        gameData.NewRun = true;
        SceneManager.LoadScene(GameStartIndex);
    }
    
    public void OnGameLost()
    {
        SceneManager.LoadScene(GameLostIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
