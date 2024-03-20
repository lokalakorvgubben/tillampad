using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    private Slider slider;
    private float initialspeed;
    public float speed;

    private void Start()
    {
        slider = GetComponent<Slider>();
        initialspeed = speed;
    }
    public void SetMaxExperience(float experience)
    {
        slider.maxValue = experience;
    }

    public void setExperience(float experience)
    {
        if(slider.value < experience)
        {
            slider.value += experience * Time.deltaTime * speed;
            speed += 1 * Time.deltaTime;
        }
        else if(slider.value > experience)
        {
            speed = initialspeed;
        }
        else if(slider.value > slider.maxValue){
            slider.value = 0;
        }
    }
}
