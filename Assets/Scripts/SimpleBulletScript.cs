using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SimpleBulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public float damage = 10;
    private bool isEnabled = true;
    private float zRotation;
    private PlayerMovement PlayerMovement;
    private EnemyScript EnemyScript;
    private WeepingAngels WeepingAngels;
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

    public void Initialize(float GunDamage, float Speed)
    {
        damage = GunDamage;
        bulletSpeed = Speed;
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
        Debug.Log("hit");
        Debug.Log(collision.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("MEGA MEGA SUCESS");
        }
        if (collision.gameObject.GetComponent<EnemyScript>() && isEnabled)
        {
            var enemy = collision.gameObject.GetComponent<EnemyScript>();
            zRotation = transform.localRotation.eulerAngles.z;
            enemy.TakeDamage(damage);
            enemy.ApplyElement(isFire, isLightning, lightningJumps, isWind, zRotation);
            isEnabled = false;
            Invoke("KillProjectile", 0.1f);
        }
        //playerMovement.playerHealth = playerMovement.playerHealth - damage;
    }
    public void KillProjectile(){
        Destroy(gameObject);
    }
}
