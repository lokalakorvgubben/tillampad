using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBolt : MonoBehaviour
{
    public float bulletSpeed;
    public float damage = 10;
    private bool isEnabled = true;
    private float zRotation;

    public GameObject spawner;
    void Start()
    {
        Invoke("KillProjectile", 5);
    }
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * bulletSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyScript>() && collision.gameObject != spawner)
        {
            var enemy = collision.gameObject.GetComponent<EnemyScript>();
            zRotation = transform.localRotation.eulerAngles.z;
            enemy.TakeDamage(damage);
            enemy.ApplyElement(false, true, 1, false, zRotation);
            isEnabled = false;
            Invoke("KillProjectile", 0.1f);
        }
    }
    public void KillProjectile()
    {
        Destroy(gameObject);
    }
}
