using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlareScript : MonoBehaviour
{

    public float flareSpeed = 5;
    private float finalFlareSpeed;
    private EnemyScript EnemyScript;
    private bool isEnabled = true;
    public float damage = 5;
    public float rotationSpeed = 25;
    public float speedSlowdown;
    private bool firstEnemy;
    float x, y, z;
    GameObject lightObject;
    Light2D flarelight;

    // Start is called before the first frame update
    void Start()
    {
        lightObject = GameObject.Find("FlareLight");
        flarelight = lightObject.GetComponent<Light2D>();
        finalFlareSpeed = Random.Range(-2.5f, 2.5f) + flareSpeed;
        Invoke("KillProjectile", 10);
        x = transform.localScale.x;
        y = transform.localScale.y;
        z = transform.localScale.z;
        //Debug.Log(transform.localRotation.eulerAngles.z);   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * finalFlareSpeed);
        finalFlareSpeed -= finalFlareSpeed * speedSlowdown * Time.deltaTime;

        /*
        transform.localScale = new Vector3(x, y, z);
        x = Mathf.Clamp(x, 0, 100);
        y = Mathf.Clamp(y, 0, 100);
        z = Mathf.Clamp(z, 0, 100);
        x -= 0.003f;
        y -= 0.003f;
        z -= 0.003f;
        flarelight.pointLightOuterRadius -= 0.001f;
        flarelight.intensity -= 0.001f;
        if(x <= 0)
        {
            Destroy(gameObject);
        }
        */
        if (flareSpeed < 0.2f)
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


            //Fixa så att detta scalear fire objektet under enemyn med 0.1x per flare.
            //Kanske till och med ändra huen på elden.
            enemy.ApplyFlare(damage);

            if (!firstEnemy)
            {
                firstEnemy = true;
            }
            else
            {
                //isEnabled = false;
            }
            Invoke("KillProjectile", 2.5f);
        }
        //playerMovement.playerHealth = playerMovement.playerHealth - damage;
    }
    public void KillProjectile()
    {
        Destroy(gameObject);
    }
}
