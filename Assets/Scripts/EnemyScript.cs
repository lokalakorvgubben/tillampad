using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyScript : MonoBehaviour
{

    public float speed = 1;
    public float enemyHealth = 100;
    public GameObject player;
    public bool onFire = false;
    private float time;

    //Desired Distance for Enemy to Attack
    public float desiredDistanceX = 0;
    public float desiredDistanceY = 0;
    public Animator anim;

    //If the enemy will stop moving after attacking
    public bool StandstillAttack = false;
    public bool Attack = false;
    public float TimeUntilAttack;
    public float TimeUntilHit;
    public float recoil;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        time = TimeUntilAttack;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        time += Time.deltaTime;

        //Distance of X and Y
        float distanceX = Mathf.Abs((player.transform.position - transform.position).x);
        float distanceY = Mathf.Abs((player.transform.position - transform.position).y);

        Vector2 LocationToMove = new Vector2(player.transform.position.x + desiredDistanceX, player.transform.position.y + desiredDistanceY);

        if(distanceY == desiredDistanceY && distanceX == desiredDistanceX && !Attack && StandstillAttack && time > TimeUntilAttack)
        {
            Invoke("StandstillHit", TimeUntilHit);
        }
        else if(distanceX == desiredDistanceX && distanceY == desiredDistanceY && !Attack && !StandstillAttack && time > TimeUntilAttack)
        {
            Invoke("MovingHit", TimeUntilHit);
        }
        else if(!StandstillAttack || !Attack)
        {
            transform.position = Vector2.MoveTowards(transform.position, LocationToMove, step);
        }
        else if (Attack && StandstillAttack)
        {
            Debug.Log("Attack");
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, LocationToMove, step);
            anim.SetBool("Attack", true);
            Attack = true;
            Invoke("CancelAttack", recoil);
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
        //This function will apply element, we will probably use our update function �r timed update do apply effects etc.
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

    void StandstillHit()
    {
        anim.SetBool("Attack", true);
        Attack = true;
        Invoke("CancelAttack", recoil);
    }

    void MovingHit()
    {
        anim.SetBool("Attack", true);
        Attack = true;
    }

    void CancelAttack()
    {
        Attack = false;
        anim.SetBool("Attack", false);
        time = 0;
    }
}
