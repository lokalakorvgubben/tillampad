using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float checkInterval = 0.5f;

    private PlayerHealth health;

    private void Start()
    {
        InvokeRepeating("CheckForEnemies", 0f, checkInterval);
        health = FindAnyObjectByType<PlayerHealth>();
    }

    private void CheckForEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            EnemyScript enemyScript = collider.GetComponent<EnemyScript>();
            if (enemyScript != null)
            {
                health.TakeDamage(enemyScript.damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the detection radius in the scene view for visual reference
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}