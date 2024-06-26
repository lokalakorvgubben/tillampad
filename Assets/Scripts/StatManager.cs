using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{

    [Header("Base Stats")]
    [SerializeField]
    public float playerLevel = 1;
    public float playerSpeedMult = 1;
    public float playerMaxMana = 100;
    public float playerManaRegen = 1;
    public float playerMaxHealth = 100;
    public float playerRegen = 1;
    public float damageMultiplier;

    [Header("Advanced Stats")]
    [SerializeField]
    public int lightningJumps = 3;
    public float flaresAmount = 3;
    public float thunderBoltAmount = 3;

    public int totalKills;


    public void RaiseStats()
    {
        playerManaRegen++;
    }
}
