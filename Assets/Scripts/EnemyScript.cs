using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyScript : MonoBehaviour
{
    public bool WeepingAngel;
    public float speed = 1;
    public float onLightningSpeedMult = 1;
    private float speedMult;
    public float enemyHealth = 100;
    public GameObject player;
    public bool onFire = false;
    public bool onLightning = false;
    private float time;
    public bool InSight;

    //Desired Distance for Enemy to Attack
    public float desiredDistanceX = 0;
    public float desiredDistanceY = 0;
    public Animator anim;

    //If the enemy will stop moving after attacking
    public bool StandstillAttack = false;
    public bool Attack = false;
    public float TimeUntilAttack;
    public float TimeUntilHit;
    NavMeshAgent agent;

    //Time between attacks
    public float recoil;

    public GameObject lightning;
    public GameObject flare;
    public float flareFireScale = 1;
    private GameObject fireEffect;
    private GameObject lightningEffect;
    private ParticleSystem firePs;

    public Transform targets;

    private float chainDamage = 10;

    private float timer = 0;
    public float graceTimer = 1.5f;

    //used to flip character
    private float positionToPlayer;
    public SpriteRenderer SpriteRenderer;

    //spawns electricity and similar stuff under this transform
    public GameObject effects;
    public GameObject Flare;
    public GameObject ExperiencePoint;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        effects = GameObject.Find("Effects");
        time = TimeUntilAttack;

        agent = GetComponent<NavMeshAgent>();

        fireEffect = transform.Find("Fire").gameObject;
        lightningEffect = transform.Find("Lightning").gameObject;
        firePs = transform.Find("Fire").gameObject.GetComponent<ParticleSystem>();
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

        //invert playermodel dependant on position to player
        positionToPlayer = transform.position.x - player.transform.position.x;
        //Debug.Log(positionToPlayer);
        
        if(positionToPlayer > 0)
        {
            SpriteRenderer.flipX = true;
        }
        else if(positionToPlayer < 0){
            SpriteRenderer.flipX = false;
        }

        float step = speed * speedMult * Time.deltaTime;
        time += Time.deltaTime;

        if (onLightning)
        {
            timer += Time.deltaTime;
            if (timer > graceTimer)
            {
                //Debug.Log("REMOVE ONLIGHTNIGN");
                CancelLightning();
                timer = 0;
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        //Distance of X and Y
        float distanceX = Mathf.Abs((player.transform.position - transform.position).x);
        float distanceY = Mathf.Abs((player.transform.position - transform.position).y);

        Vector2 LocationToMove = new Vector3(player.transform.position.x + desiredDistanceX, player.transform.position.y + desiredDistanceY, 0);
        if (!WeepingAngel)
        {
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
                agent.SetDestination(LocationToMove);
            }
            else if (Attack && StandstillAttack)
            {
                //Debug.Log("Attack");
            }
            else
            {
                agent.SetDestination(LocationToMove);
                anim.SetBool("Attack", true);
                Attack = true;
                Invoke("CancelAttack", recoil);
            }
        }
        else if (WeepingAngel)
        {
            if (!InSight)
            {
                agent.SetDestination(LocationToMove);
            }
        }


        if(enemyHealth <= 0)
        {
            DropXP();
            Die();
        }

    }
    private void FixedUpdate()
    {
        if(onFire == true)
        {
            enemyHealth -= 0.1f * flareFireScale;
            //Debug.Log(enemyHealth);
        }
    }
    void DropXP()
    {
        //maybe call function here to the xp to assign xp value depending on what died
        Instantiate(ExperiencePoint, gameObject.transform.position, gameObject.transform.rotation, effects.transform);
    }
    void Die()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
    }

    public void TemporaryThunderDamage()
    {
        enemyHealth -= 25;
        Debug.Log("THUNDER!");
        Invoke("KillCloud", 0.6f);
    }
    public void KillCloud()
    {
        lightningEffect.SetActive(false);
    }
    public void ApplyFlare(float damage)
    {
        Debug.Log("Apply Flare");
        enemyHealth -= damage;
        flareFireScale += 0.1f;
        onFire = true;
        fireEffect.SetActive(true);
        firePs.startSize += 0.1f;
        Invoke("CancelFire", 4);
    }
    public void ApplyElement(bool isFire, bool isLightning, int lightningJumps, bool isWind, float zRotation)
    {
        //This function will apply element, we will probably use our update function �r timed update do apply effects etc.
        if (isFire == true)
        {
            Debug.Log("Fire works");
            onFire = true;
            fireEffect.SetActive(true);
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
        if (isWind == true)
        {
            Debug.Log("Wind Works");
        }


        //-----------------------------------//
        //-We start doing interactions below-//
        //-----------------------------------//
        if(onLightning && isWind)
        {
            lightningEffect.SetActive(true);
            Invoke("TemporaryThunderDamage", 1.8f);
        }
        
        if(onFire && isWind)
        {
            Debug.Log("spawn flare");
            Debug.Log(zRotation);
            SpawnFlares(zRotation);
        }
    }
    public void SpawnFlares(float zRotation)
    {
        //Instantiate()
        Debug.Log("FUCKING NUKE");

        //GameObject newFlareObject = Instantiate(Flare, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(-50, 50) + zRotation)), effects.transform);
        for (int i = 0; i < 10; i++)
        {
            Instantiate(Flare, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(-40, 40) + zRotation)), effects.transform);
        }
        //Debug.Log(newFlareObject);


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

            GameObject newLightningObject = Instantiate(lightning, effects.transform);
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
        fireEffect.SetActive(false);
        flareFireScale = 1;
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
