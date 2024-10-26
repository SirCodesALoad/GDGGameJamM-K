using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreCalculator : MonoBehaviour
{
    [SerializeField] private DataStore gameData;
    private TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        Text = transform.GetComponent<TextMeshProUGUI>();
        int score = 0;

        score = gameData.CurrentRegAmmo * 100;
        score += gameData.CurrentExpAmmo * 100;
        score += gameData.CurrentPenAmmo * 100;
        score += gameData.CurrentRicAmmo * 100;
        Text.SetText("Score: " + score.ToString());
    }

}
