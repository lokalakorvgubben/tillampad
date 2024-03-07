using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fps : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;
    public float updateInterval = 1.0f;
    public int maxFPS;
    private float Lowest = 60;

    private void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        StartCoroutine(UpdateFPSCounter());
        Application.targetFrameRate = maxFPS;
    }

    private void Update()
    {
        if (Mathf.RoundToInt(1f / Time.deltaTime) < Lowest)
        {
            Lowest = 1f / Time.deltaTime;
        }
    }
    private IEnumerator UpdateFPSCounter()
    {
        while (true)
        {
            m_TextMeshProUGUI.text = Mathf.RoundToInt(1f / Time.deltaTime).ToString() + "\n" + Lowest;
            yield return new WaitForSeconds(updateInterval);
        }
    }
}
