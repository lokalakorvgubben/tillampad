using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenu;
    public void Play()
    {
        SceneManager.LoadScene("Jacob_Dungeon", LoadSceneMode.Single);
    }
    public void Options()
    {
        OptionsMenu.SetActive(true);
    }
    public void ExitToDesktop()
    {
        Application.Quit();
    }
}
