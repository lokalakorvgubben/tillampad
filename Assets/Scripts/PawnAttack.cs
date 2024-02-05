using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnAttack : MonoBehaviour
{
    public EnemyScript enemy;
    public float damage;
    private CircleCollider2D circleCollider;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if(enemy.Attack == true)
        {
            CheckCollisions();
        }
    }

    public void CheckCollisions()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<PlayerHealth>())
            {
                var player = collider.gameObject.GetComponent<PlayerHealth>();
                player.TakeDamage(damage);
            }
        }
    }
}
