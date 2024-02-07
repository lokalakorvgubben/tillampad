using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyScript : MonoBehaviour
{

    public float speed = 1;
    public float onLightningSpeedMult = 1;
    private float speedMult;
    public float enemyHealth = 100;
    public GameObject player;
    public bool onFire = false;
    public bool onLightning = false;
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

    //Time between attacks
    public float recoil;

    public GameObject lightning;

    public Transform targets;

    private float chainDamage = 10;

    private float timer = 0;
    public float graceTimer = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        time = TimeUntilAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if(onLightning == true)
        {
            speedMult = onLightningSpeedMult;
        }
        else
        {
            speedMult = 1f;
        }



        float step = speed * speedMult * Time.deltaTime;
        time += Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > graceTimer)
        {
            CancelLightning();
            timer = 0;
        }

        //Distance of X and Y
        float distanceX = Mathf.Abs((player.transform.position - transform.position).x);
        float distanceY = Mathf.Abs((player.transform.position - transform.position).y);

        Vector2 LocationToMove = new Vector2(player.transform.position.x + desiredDistanceX, player.transform.position.y + desiredDistanceY);

        if(distanceY == desiredDistanceY && distanceX == desiredDistanceX && !Attack && StandstillAttack && time > TimeUntilAttack)
        {
            Invoke("Hit", TimeUntilHit);
        }
        else if(distanceX == desiredDistanceX && distanceY == desiredDistanceY && !Attack && !StandstillAttack && time > TimeUntilAttack)
        {
            Invoke("Hit", TimeUntilHit);
        }
        else if(!StandstillAttack || !Attack)
        {
            transform.position = Vector2.MoveTowards(transform.position, LocationToMove, step);
        }
        else if (Attack && StandstillAttack)
        {
            //Debug.Log("Attack");
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
            //Debug.Log(enemyHealth);
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
    public void ApplyElement(bool isFire, bool isLightning, int lightningJumps)
    {
        //This function will apply element, we will probably use our update function är timed update do apply effects etc.
        if (isFire == true)
        {
            Debug.Log("Fire works");
            onFire = true;
            transform.GetChild(1).gameObject.SetActive(true);
            Invoke("CancelFire", 4);
        }
        if (isLightning == true)
        {
            Debug.Log("Lightning Wors");
            onLightning = true;
            Debug.Log("lightningon");
            ChainLightning(lightningJumps);
            //Invoke("CancelFire", 4);
        }
    }
    public void ChainLightning(int lightningJumps)
    {
        onLightning = true;
        GameObject closestTarget = FindClosestTarget();
        //Debug.Log(lightningJumps);
        if (closestTarget != null && lightningJumps > 0)
        {
            lightningJumps--;
            //Debug.Log(lightningJumps);
            //Debug.Log("Closest target position: " + closestTarget.transform.position);

            GameObject newLightningObject = Instantiate(lightning);
            LineController2 newLightning = newLightningObject.GetComponent<LineController2>();

            newLightning.AssignTarget(transform, closestTarget.transform);

            //calla function i enemyscript av closest target.
            var nextEnemy = closestTarget.gameObject.GetComponent<EnemyScript>();
            nextEnemy.TakeDamage(chainDamage);
            nextEnemy.ChainLightning(lightningJumps);

        }
        else
        {
            //Debug.Log("No other enemy found");
        }
    }

    GameObject FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject target in targets)
        {
            if (target == this.gameObject || target.gameObject.GetComponent<EnemyScript>().onLightning == true)
            {
                continue;
            }

            Vector3 diff = target.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = target;
                distance = curDistance;
            }
        }
        return closest;
    }

    void CancelFire()
    {
        onFire = false;
        transform.GetChild(1).gameObject.SetActive(false);
    }
    void CancelLightning()
    {
        onLightning = false;
    }

    void Hit()
    {
        anim.SetBool("Attack", true);
        Attack = true;
        Invoke("CancelAttack", recoil);
    }

    void CancelAttack()
    {
        Attack = false;
        anim.SetBool("Attack", false);
        time = 0;
    }
}
