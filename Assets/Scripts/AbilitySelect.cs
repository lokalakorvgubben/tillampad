using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelect : MonoBehaviour
{
    private CardRandomizer cardRandomizer;
    public bool selectAbilities;
    public GameObject cardAbilities;
    public Texture2D cursor;
    public GameObject PauseMenu;
    public GameObject DeathUI;
    public bool paused = false;
    private PlayerHealth player;
    private GameObject InGameUI;

    private List<CardRandomizer> cardRandomizers = new List<CardRandomizer>();

    void Start()
    {
        FindCardRandomizers();
        player = FindAnyObjectByType<PlayerHealth>();
        InGameUI = transform.Find("InGameUI").gameObject;
    }

    void FindCardRandomizers()
    {
        GameObject[] allGameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allGameObjects)
        {
            CardRandomizer[] scripts = obj.GetComponents<CardRandomizer>();
            cardRandomizers.AddRange(scripts);
        }
        cardAbilities.SetActive(false);
    }

    public void LevelCards()
    {
        cardAbilities.SetActive(true);
        Invoke(nameof(shuffle), 0.00001f);
    }
    void Update()
    {
        if(cardAbilities.activeSelf == false && PauseMenu.activeSelf == false)
        {
            Time.timeScale = 1f;
            paused = false;
        }
        else if(cardAbilities.activeSelf == true || PauseMenu.activeSelf == true)
        {
            Time.timeScale = 0f;
            paused = true;
        }



        if(player.health <= 0)
        {
            paused = true;
            Time.timeScale = 0f;
            DeathUI.SetActive(true);
            InGameUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(true);
        }

        if(!paused)
        {
            Cursor.SetCursor(cursor, new Vector2(80, 80), CursorMode.Auto);
        }
        else if(paused)
        {
            Cursor.SetCursor(null, new Vector2(0f, 0f), CursorMode.Auto);
        }
    }

    private void shuffle()
    {
        foreach(CardRandomizer cardRandomizer in cardRandomizers)
        {
            cardRandomizer.GetCard();
        }
    }
}
