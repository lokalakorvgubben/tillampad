using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmScript : MonoBehaviour
{
    [Header("References")]
    public GameObject fireBullet;
    public GameObject lightningBullet;
    public GameObject windBullet;
    public Transform gunPoint; // The point from where bullets are fired
    private GameObject bullets;
    private AbilitySelect pausing;
    private StatManager stats;
    private GameObject sprite;

    [Header("Bool")]
    public bool isLeftArm; // Indicates if this script is controlling the left arm
    private bool shooting;
    public bool allowButtonHold; // Indicates if holding the button is allowed for continuous shooting

    [Header("Gun Stats")]
    private Mana mana;
    public float manaToShoot; // Mana cost per shot
    private float ShootTime = 0;
    [Range(0, 10)] public float TimeToShoot = 1;
    public float spread; // Bullet spread angle
    public int bulletsToShoot = 1; // Number of bullets to shoot at once
    private float GunDamage; // Calculated gun damage
    public float damage = 1;
    public float angle; // Shooting angle
    public float bulletspeed = 5; // Speed of the bullets

    public enum BulletType
    {
        Fire,
        Lightning,
        Wind,
        // Add more bullet types as needed
    }

    public BulletType currentBulletType = BulletType.Fire; // Current selected bullet type

    // Find All Objects
    void Start()
    {
        stats = FindAnyObjectByType<StatManager>();
        bullets = GameObject.Find("Bullets");
        pausing = FindAnyObjectByType<AbilitySelect>();
        mana = FindAnyObjectByType<Mana>();
        ShootTime = TimeToShoot;
        sprite = transform.Find("Gun").gameObject;
    }

    void Update()
    {
        // Determine shooting based on mouse button and whether button hold is allowed
        if (!isLeftArm)
        {
            if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse1);
            else shooting = Input.GetKeyDown(KeyCode.Mouse1);
        }
        else if (isLeftArm)
        {
            if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        // If the game is not paused
        if (pausing.paused == false)
        {
            Vector3 mouse_pos; // Position of the mouse
            Vector3 object_pos; // Position of the object

            mouse_pos = Input.mousePosition; // Get mouse position
            object_pos = Camera.main.WorldToScreenPoint(transform.position); // Get object position in screen coordinates
            ShootTime += Time.deltaTime; // Increment shoot timer
            
            // Flip the sprite based on the mouse position
            if (mouse_pos.x < object_pos.x)
            {
                sprite.transform.localScale = new Vector3(sprite.transform.localScale.x, -5, transform.localScale.z);
            }
            else
            {
                sprite.transform.localScale = new Vector3(sprite.transform.localScale.x, 5, transform.localScale.z);
            }

            // Calculate the angle to the mouse position
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            // If the shoot timer has elapsed and there's enough mana, shoot
            if (ShootTime > TimeToShoot && mana.mana >= manaToShoot)
            {
                if (shooting)
                {
                    DamageMult(); // Calculate gun damage with multiplier
                    mana.mana -= manaToShoot; // Deduct mana cost
                    mana.shoot = true; // Indicate shooting
                    for (int i = 0; i < bulletsToShoot; i++)
                    {
                        float x = Random.Range(-spread, spread); // Apply bullet spread
                        GameObject bulletPrefab = GetBulletPrefab(); // Get the bullet prefab based on current type
                        // Instantiate the bullet and initialize its properties
                        Instantiate(bulletPrefab, gunPoint.position, Quaternion.Euler(new Vector3(0, 0, angle + x)), bullets.transform)
                            .GetComponent<SimpleBulletScript>().Initialize(GunDamage, bulletspeed);
                    }

                    ShootTime = 0; // Reset shoot timer
                }

                // TEST, not FINAL, shooting condition for space key
                if (Input.GetKeyDown(KeyCode.Space) && !isLeftArm)
                {
                    for (int i = 0; i < bulletsToShoot; i++)
                    {
                        float x = Random.Range(-spread, spread); // Apply bullet spread
                        Instantiate(fireBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + x)), bullets.transform); // Fire bullet
                        Instantiate(windBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + x)), bullets.transform) // Wind bullet
                            .GetComponent<SimpleBulletScript>().Initialize(GunDamage, bulletspeed);
                    }

                    ShootTime = 0; // Reset shoot timer
                }
            }
        }
    }

    // Method to get the bullet prefab based on current bullet type
    GameObject GetBulletPrefab()
    {
        switch (currentBulletType)
        {
            case BulletType.Fire:
                return fireBullet;
            case BulletType.Lightning:
                return lightningBullet;
            case BulletType.Wind:
                return windBullet;
            // Add more cases for additional bullet types
            default:
                return fireBullet; // Default to fireBullet if the type is not recognized
        }
    }

    // Method to calculate gun damage with multiplier
    public void DamageMult()
    {
        if (stats.damageMultiplier != 0)
        {
            GunDamage = damage * stats.damageMultiplier;
        }
        else
        {
            GunDamage = damage;
        }
    }
}
