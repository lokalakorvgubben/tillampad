using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float maxMana;
    public float mana;
    public float manaRecovery;
    private ManaBar manaBar;

    private void Start()
    {
        mana = maxMana;
        manaBar = GameObject.FindAnyObjectByType<ManaBar>();
    }

    private void Update()
    {
        mana = Mathf.Clamp(mana, 0, maxMana);
        manaBar.SetMaxMana(maxMana);
        manaBar.setMana(mana);
        mana += manaRecovery;

    }
}
