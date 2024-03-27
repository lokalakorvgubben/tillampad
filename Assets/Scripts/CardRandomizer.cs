using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.UI;

public class CardRandomizer : MonoBehaviour
{
    private CardsHolder cardhold;
    private GameObject CardsSelector;
    private TextMeshProUGUI name;
    private TextMeshProUGUI description;

    private ArmScript RightHand;
    private ArmScript LeftHand;
    private StatManager stats;
    private Image image;

    private float DamageIncrease;
    private bool bulletsmultiply;
    private int bulletsAdded;
    private float HealthIncrease;
    private float healingFactor;
    private float maxMana;
    private float manaRegen;
    private bool leftArm;
    private bool rightArm;
    private bool fire;
    private bool lightning;
    private bool wind;
    private int totalFlares;
    private int lightningJumps;

    private void Start()
    {
        RightHand = GameObject.Find("Player/Arms/RightArm").GetComponent<ArmScript>();
        LeftHand = GameObject.Find("Player/Arms/LeftArm").GetComponent<ArmScript>();
        stats = FindAnyObjectByType<StatManager>();
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
        DamageIncrease = card.DamageXIncrease;
        bulletsmultiply = card.bulletsMultiply;
        bulletsAdded = card.bulletsAdded;
        HealthIncrease = card.HealthXIncrease;
        healingFactor = card.healingFactor;
        leftArm = card.leftArm;
        rightArm = card.rightArm;
        fire = card.fire;
        lightning = card.lightning;
        wind = card.wind;
        totalFlares = card.totalFlares;
        lightningJumps = card.lightningJumps;
        manaRegen = card.ManaRegen;
        maxMana = card.ManaXIncrease;
    }

    public void RecieveCard()
    {
        CardsSelector.SetActive(false);
        if(HealthIncrease > 0)
        {
            stats.playerMaxHealth *= HealthIncrease;
        }
        if(healingFactor > 0)
        {
            stats.playerRegen *= healingFactor;
        }
        if(manaRegen > 0)
        {
            stats.playerManaRegen *= manaRegen;
        }
        if(maxMana > 0)
        {
            stats.playerMaxMana *= maxMana;
        }
        stats.flaresAmount += totalFlares;
        stats.lightningJumps += lightningJumps;

        if (leftArm)
        {
            if(DamageIncrease > 0)
            {
                LeftHand.GunDamage *= DamageIncrease;
            }
            if(bulletsmultiply)
            {
                LeftHand.bulletsToShoot *= bulletsAdded;
            }
            else if(!bulletsmultiply)
            {
                LeftHand.bulletsToShoot += bulletsAdded;
            }
            if (fire)
            {
                LeftHand.currentBulletType = ArmScript.BulletType.Fire;
            }
            else if (lightning)
            {
                LeftHand.currentBulletType = ArmScript.BulletType.Lightning;
            }
            else if (wind)
            {
                LeftHand.currentBulletType = ArmScript.BulletType.Wind;
            }
        }
        if (rightArm)
        {
            if(DamageIncrease > 0)
            {
                RightHand.GunDamage *= DamageIncrease;
            }
            if (bulletsmultiply)
            {
                RightHand.bulletsToShoot *= bulletsAdded;
            }
            else if (!bulletsmultiply)
            {
                RightHand.bulletsToShoot += bulletsAdded;
            }
            if (fire)
            {
                RightHand.currentBulletType = ArmScript.BulletType.Fire;
            }
            else if (lightning)
            {
                RightHand.currentBulletType = ArmScript.BulletType.Lightning;
            }
            else if (wind)
            {
                RightHand.currentBulletType = ArmScript.BulletType.Wind;
            }
        }
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
