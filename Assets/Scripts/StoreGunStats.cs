using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreGunStats : MonoBehaviour
{
    public float manaToShoot;
    [Range(0, 10)] public float TimeToShoot = 1;
    public float spread;
    public int bulletsToShoot = 1;
    public float damage = 1;
    public float bulletspeed = 5;
    public Sprite gunSprite;

    private ArmScript leftHand;
    private ArmScript rightHand;

    private TextMeshProUGUI textmesh;
    private AbilitySelect pausing;

    private void Start()
    {
        pausing = FindAnyObjectByType<AbilitySelect>();
        leftHand = GameObject.Find("LeftArm").GetComponent<ArmScript>();
        rightHand = GameObject.Find("RightArm").GetComponent<ArmScript>();
        textmesh = transform.Find("Background/Stats").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textmesh.text = "Damage = " + damage + "\n" +
            "Bullets To Shoot = " + bulletsToShoot + "\n" +
            "Spread = " + spread + "\n" +
            "Time To Shoot = " + TimeToShoot + "\n" +
            "Mana Per Shot = " + manaToShoot;
    }

    public void LeftHand()
    {
        leftHand.manaToShoot = manaToShoot;
        leftHand.spread = spread;
        leftHand.damage = damage;
        leftHand.bulletspeed = bulletspeed;
        leftHand.bulletsToShoot = bulletsToShoot;
        leftHand.TimeToShoot = TimeToShoot;
        gameObject.SetActive(false);
    }

    public void RightHand()
    {
        rightHand.manaToShoot = manaToShoot;
        rightHand.spread = spread;
        rightHand.damage = damage;
        rightHand.bulletspeed = bulletspeed;
        rightHand.bulletsToShoot = bulletsToShoot;
        rightHand.TimeToShoot = TimeToShoot;
        gameObject.SetActive(false);
    }

    public void Decline()
    {
        gameObject.SetActive(false);
    }
}
