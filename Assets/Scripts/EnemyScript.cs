using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyScript : MonoBehaviour
{

    public float speed = 1;
    public float enemyHealth = 100;
    public GameObject player;
    public bool onFire = false;
    public float desiredDistance = 0;
    public Animator anim;
    public bool StandstillAttack = false;
    private bool Attack = false;
    public float recoil;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        float distance = Vector2.Distance(player.transform.position, transform.position);


        if(distance <= desiredDistance)
        {
            anim.SetTrigger("Attack");
            Attack = true;
            Invoke("CancelAttack", recoil);
        }
        else if(!StandstillAttack || !Attack)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
        }

        if(enemyHealth <= 0)
        {
            Die();
        }

    }
    private void FixedUpdate()
    {
        if(onFire == true)
        {
            enemyHealth -= 0.1f;
            Debug.Log(enemyHealth);
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
    }
    public void ApplyElement(bool isFire)
    {
        //This function will apply element, we will probably use our update function är timed update do apply effects etc.
        if(isFire == true)
        {
            Debug.Log("DET FUNKAAAAAAAAAAAAAAAAR");
            onFire = true;
            transform.GetChild(1).gameObject.SetActive(true);
            Invoke("CancelFire", 4);
        }
    }

    void CancelFire()
    {
        onFire = false;
        transform.GetChild(1).gameObject.SetActive(false);
    }

    void CancelAttack()
    {
        Attack = false;
    }
}
