using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SimpleBulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public float damage = 10;
    private bool isEnabled = true;
    private PlayerMovement PlayerMovement;
    private EnemyScript EnemyScript;
    //public bool[] elements;
    //Comment what value = what element
    //elements[0] = fire

    public bool isFire = false;
    public bool isLightning = false;
    public bool isWind = false;
    public int lightningJumps = 0;

    void Start()
    {
        Invoke("KillProjectile", 5);
    }

    public void Initialize(float GunDamage)
    {
        damage = GunDamage;
        Invoke("KillProjectile", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(isWind)
        {
            bulletSpeed += bulletSpeed * 3 * Time.deltaTime;
        }
        transform.Translate(Vector2.right * Time.deltaTime * bulletSpeed);
        //Debug.Log(damage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("MEGA MEGA SUCESS");
        }
        if (collision.gameObject.GetComponent<EnemyScript>() && isEnabled)
        {
            var enemy = collision.gameObject.GetComponent<EnemyScript>();
            enemy.TakeDamage(damage);
            enemy.ApplyElement(isFire, isLightning, lightningJumps, isWind);
            isEnabled = false;
            Invoke("KillProjectile", 0.1f);

        }
        //playerMovement.playerHealth = playerMovement.playerHealth - damage;
    }
    public void KillProjectile(){
        Destroy(gameObject);
    }
}
