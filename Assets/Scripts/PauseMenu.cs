using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Continue()
    {
        Time.timeScale = 1.0f;
        transform.Find("PauseMenu").gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    private void Update()
    {
        if (transform.Find("PauseMenu").gameObject.activeSelf || transform.Find("InGameUI/WeaponSelect").gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        }
    }
}
