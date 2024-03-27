using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Continue()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {

    }

    private void Update()
    {
        if (isActiveAndEnabled)
        {
            Time.timeScale = 0f;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Continue();
        }
    }
}
