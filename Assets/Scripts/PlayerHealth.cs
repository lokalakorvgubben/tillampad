using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float Health;
    private HealthBar healthBar;

    private void Start()
    {
        healthBar = GameObject.FindAnyObjectByType<HealthBar>();
        healthBar.SetMaxHealth(Health);
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    private void Update()
    {
        if(Health <= 0)
        {
            Debug.Log("Dead");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(1);
        }
        healthBar.setHealth(Health);
    }
}
