using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float maxMana;
    public float mana;
    private float initialmanaRec;
    public float manaRecovery;
    private ManaBar manaBar;
    public bool shoot;
    public float timeuntilrecovery = 1;
    private float time;
    private TextMeshProUGUI count;

    private void Start()
    {
        initialmanaRec = manaRecovery;
        mana = maxMana;
        manaBar = GameObject.FindAnyObjectByType<ManaBar>();
        count = manaBar.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        count.text = ((int)mana) + "/" + maxMana;
        time += Time.deltaTime;
        mana = Mathf.Clamp(mana, 0, maxMana);
        manaBar.SetMaxMana(maxMana);
        manaBar.setMana(mana);
        if(shoot == true)
        {
            shoot = false;
            time = 0;
        }

        if(time > timeuntilrecovery)
        {
            mana += manaRecovery * Time.deltaTime;
            manaRecovery += 1 * Time.deltaTime;
        }
        if(time < timeuntilrecovery)
        {
            manaRecovery = initialmanaRec;
        }
        if(mana >= maxMana)
        {
            mana = maxMana;
        }
    }
}
