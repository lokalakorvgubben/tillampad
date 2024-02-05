using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float Health;
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
    }
}
