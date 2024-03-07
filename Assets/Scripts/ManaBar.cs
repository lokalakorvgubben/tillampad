using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void SetMaxMana(float mana)
    {
        slider.maxValue = mana;
    }

    public void setMana(float mana)
    {
        slider.value = mana;
    }
}
