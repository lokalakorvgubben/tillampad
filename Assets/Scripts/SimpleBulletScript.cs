using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SimpleBulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public float damage = 10;
    private PlayerMovement PlayerMovement;
    private EnemyScript EnemyScript;
    //public bool[] elements;
    //Comment what value = what element
    //elements[0] = fire

    public bool isFire = false;
    public bool isLightning = false;

    void Start()
    {
        Invoke("KillProjectile", 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * bulletSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("MEGA MEGA SUCESS");
        }
        if (collision.gameObject.GetComponent<EnemyScript>())
        {
            var enemy = collision.gameObject.GetComponent<EnemyScript>();
            enemy.TakeDamage(damage);
            enemy.ApplyElement(isFire);
            enemy.ApplyElement(isLightning);
        }
        //playerMovement.playerHealth = playerMovement.playerHealth - damage;
    }
    public void KillProjectile(){
        Destroy(gameObject);
    }
}
