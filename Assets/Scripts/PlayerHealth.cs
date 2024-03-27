using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float health;
    private HealthBar healthBar;
    private StatManager statManager;

    private void Start()
    {
        statManager = FindAnyObjectByType<StatManager>();
        healthBar = GameObject.FindAnyObjectByType<HealthBar>();

        maxHealth = statManager.playerMaxHealth;
        healthBar.SetMaxHealth(maxHealth);
        health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void Update()
    {
        maxHealth = statManager.playerMaxHealth;
        healthBar.SetMaxHealth(maxHealth);
        if (health <= 0)
        {
            Debug.Log("Dead");
        }
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.setHealth(health);
    }
}
