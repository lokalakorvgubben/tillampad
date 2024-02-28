using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareScript : MonoBehaviour
{

    public float flareSpeed = 5;
    private EnemyScript EnemyScript;
    private bool isEnabled = true;
    public float damage = 5;
    public float rotationSpeed = 25;
    public float speedSlowdown;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("KillProjectile", 5);
        Debug.Log(transform.localRotation.eulerAngles.z);   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * flareSpeed);
        flareSpeed -= flareSpeed * speedSlowdown * Time.deltaTime;

        if(flareSpeed < 0.2f)
        {
            //KillProjectile();
        }

        if (transform.localRotation.eulerAngles.z < 90f || transform.localRotation.eulerAngles.z > 270f)
        {
            Debug.Log(transform.localRotation.eulerAngles.z);
            transform.eulerAngles = new Vector3(0, 0, transform.localRotation.eulerAngles.z - rotationSpeed * Time.deltaTime);
        }
        
        if (transform.localRotation.eulerAngles.z > 90f && transform.localRotation.eulerAngles.z < 270f)
        {
            Debug.Log(transform.localRotation.eulerAngles.z);
            transform.eulerAngles = new Vector3(0, 0, transform.localRotation.eulerAngles.z + rotationSpeed * Time.deltaTime);
        }

        /*
        if (transform.localRotation.eulerAngles.z < 90f || transform.localRotation.eulerAngles.z > 270)
        {
            //Debug.Log(transform.localRotation.eulerAngles.z);
            //transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z - 5 * Time.deltaTime);
            //transform.eulerAngles = new Vector3(0, 0, transform.localRotation.eulerAngles.z - 5 * Time.deltaTime);
            Debug.Log(new Vector3(0, 0, transform.localRotation.eulerAngles.z - 5 * Time.deltaTime));
            //var rot = transform.localRotation.eulerAngles.z += 5 * Time.deltaTime;
            //Debug.Log(transform.localRotation.eulerAngles.z);
        }*/

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
            enemy.ApplyFlare(damage);
            isEnabled = false;
            Invoke("KillProjectile", 0.1f);
        }
        //playerMovement.playerHealth = playerMovement.playerHealth - damage;
    }
    public void KillProjectile()
    {
        //Destroy(gameObject);
    }
}
