using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class TotalKills : MonoBehaviour
{
    private StatManager stats;
    private TextMeshProUGUI text;


    private void Start()
    {
        stats = FindAnyObjectByType<StatManager>();
        text = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        text.text = "Total Kills = " + stats.totalKills;
    }
}
