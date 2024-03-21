using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelect : MonoBehaviour
{
    private CardRandomizer cardRandomizer;
    public bool selectAbilities;
    public GameObject cardAbilities;
    public Texture2D cursor;

    private List<CardRandomizer> cardRandomizers = new List<CardRandomizer>();

    void Start()
    {
        FindCardRandomizers();
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
        if(cardAbilities.activeSelf == false)
        {
            Time.timeScale = 1f;
            Cursor.SetCursor(cursor, new Vector2(80, 80), CursorMode.Auto);
        }
        else if(cardAbilities.activeSelf == true)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void shuffle()
    {
        foreach(CardRandomizer cardRandomizer in cardRandomizers)
        {
            cardRandomizer.GetCard();
        }
        Time.timeScale = 0f;
    }
}
