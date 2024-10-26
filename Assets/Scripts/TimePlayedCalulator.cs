using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimePlayedCalulator : MonoBehaviour
{
    [SerializeField] private DataStore gameData;
    private TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        Text = transform.GetComponent<TextMeshProUGUI>();
        Text.SetText("Time: " + (gameData.GameTime.ToString()));
    }

}