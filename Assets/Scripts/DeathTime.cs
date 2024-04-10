using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathTime : MonoBehaviour
{
    private EnemySpawner timer;
    private TextMeshProUGUI text;

    void Start()
    {
        timer = FindAnyObjectByType<EnemySpawner>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.SecondsToShow < 10)
        {
            text.text = timer.MinutesToShow.ToString() + ":0" + ((int)timer.SecondsToShow).ToString();
        }
        else
        {
            text.text = timer.MinutesToShow.ToString() + ":" + ((int)timer.SecondsToShow).ToString();
        }
    }
}
