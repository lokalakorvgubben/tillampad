using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareScript : MonoBehaviour
{

    public float flareSpeed = 5;
    private float finalFlareSpeed;
    private EnemyScript EnemyScript;
    private bool isEnabled = true;
    public float damage = 5;
    public float rotationSpeed = 25;
    public float speedSlowdown;

    // Start is called before the first frame update
    void Start()
    {
        finalFlareSpeed = Random.Range(-2.5f, 2.5f) + flareSpeed;
        Invoke("KillProjectile", 5);
        //Debug.Log(transform.localRotation.eulerAngles.z);   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * finalFlareSpeed);
        finalFlareSpeed -= finalFlareSpeed * speedSlowdown * Time.deltaTime;

        if(flareSpeed < 0.2f)
        {
            //KillProjectile();
        }

        if (transform.localRotation.eulerAngles.z < 90f || transform.localRotation.eulerAngles.z > 270f)
        {
            //Debug.Log(transform.localRotation.eulerAngles.z);
            transform.eulerAngles = new Vector3(0, 0, transform.localRotation.eulerAngles.z - rotationSpeed * Time.deltaTime);
        }
        
        if (transform.localRotation.eulerAngles.z > 90f && transform.localRotation.eulerAngles.z < 270f)
        {
            //Debug.Log(transform.localRotation.eulerAngles.z);
            transform.eulerAngles = new Vector3(0, 0, transform.localRotation.eulerAngles.z + rotationSpeed * Time.deltaTime);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("JNAJFSKNSANASOJNFNJASLJNSAJNFS");
        if (collision.gameObject.GetComponent<EnemyScript>() && isEnabled)
        {
            Debug.Log("FLAREATTACK");
            var enemy = collision.gameObject.GetComponent<EnemyScript>();
            enemy.ApplyFlare(damage);
            isEnabled = false;
            Invoke("KillProjectile", 2.5f);
        }
        //playerMovement.playerHealth = playerMovement.playerHealth - damage;
    }
    public void KillProjectile()
    {
        Destroy(gameObject);
    }
}
