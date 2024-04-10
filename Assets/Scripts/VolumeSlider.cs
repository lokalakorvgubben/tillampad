using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private TextMeshProUGUI Volume;
    private Slider volumeSlider;

    private void Start()
    {
        volumeSlider = GetComponentInChildren<Slider>();
        Volume = transform.Find("Volume").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        AudioListener.volume = volumeSlider.value;
        float VolumeValue = volumeSlider.value * 100;
        Volume.text = ((int)VolumeValue).ToString();
    }
}
