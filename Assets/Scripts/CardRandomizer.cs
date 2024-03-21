using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardRandomizer : MonoBehaviour
{
    private CardsHolder cardhold;
    private GameObject CardsSelector;
    private TextMeshProUGUI name;
    private TextMeshProUGUI description;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        CardsSelector = GameObject.Find("AbilitySelector");
        cardhold = FindAnyObjectByType<CardsHolder>();
        name = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        description = transform.Find("Description").GetComponent<TextMeshProUGUI>();
    }

    public void GetCard()
    {
        CardsHolder.Card card = GetRandomCard();
        name.text = card.Name;
        description.text = card.Description;
        if(card.cursed == true)
        {
            image.color = cardhold.cursed;
        }
        else if(card.bigandcoolcard == true)
        {
            image.color = cardhold.blessed;
        }
        else
        {
            image.color = cardhold.normal;
        }
    }

    public void RecieveCard()
    {
        CardsSelector.SetActive(false);
    }

    private CardsHolder.Card GetRandomCard()
    {
        if (cardhold.cards.Count > 0)
        {
            float totalWeight = 0f;

            foreach (CardsHolder.Card variant in cardhold.cards)
            {
                totalWeight += variant.spawnWeight;
            }

            float randomValue = Random.Range(0f, totalWeight);

            foreach (CardsHolder.Card variant in cardhold.cards)
            {
                if (randomValue <= variant.spawnWeight)
                {
                    return variant;
                }

                randomValue -= variant.spawnWeight;
            }
        }

        return null;
    }
}
