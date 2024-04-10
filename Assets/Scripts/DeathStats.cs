using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathStats : MonoBehaviour
{
    private StatManager stats;
    private TextMeshProUGUI text;
    private PlayerLevel Level;
    void Start()
    {
        stats = FindAnyObjectByType<StatManager>();
        text = GetComponent<TextMeshProUGUI>();
        Level = FindAnyObjectByType<PlayerLevel>();
    }
    void Update()
    {
        text.text = "Level = " + Level.level +
            "\n" + "Health = " + stats.playerMaxHealth +
            "\n" + "Mana = " + stats.playerMaxHealth +
            "\n" + "Health Regen = " + stats.playerRegen +
            "\n" + "Mana Regen = " + stats.playerManaRegen +
            "\n" + "Damage Multiplier = " + stats.damageMultiplier;
    }
}
