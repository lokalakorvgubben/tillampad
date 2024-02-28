using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour
{
    public Transform gunPoint;
    public bool isLeftArm;
    public GameObject fireBullet;
    public GameObject lightningBullet;
    private float ShootTime = 0;
    [Range(0, 10)] public float TimeToShoot = 1;
    public float spread;
    public int bulletsToShoot = 1;
    public float GunDamage;
    public float angle;
    private GameObject bullets;
    private bool Leftshooting;
    private bool Rightshooting;
    public bool allowLeftButtonHold;
    public bool allowRightButtonHold;

    // Start is called before the first frame update
    void Start()
    {
        bullets = GameObject.Find("Bullets");
        ShootTime = TimeToShoot;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowLeftButtonHold) Leftshooting = Input.GetKey(KeyCode.Mouse0);
        else Leftshooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(allowRightButtonHold) Rightshooting = Input.GetKey(KeyCode.Mouse1);
        else Rightshooting = Input.GetKeyDown(KeyCode.Mouse1);

        Vector3 mouse_pos;
        Vector3 object_pos;

        mouse_pos = Input.mousePosition;
        object_pos = Camera.main.WorldToScreenPoint(transform.position);
        ShootTime += Time.deltaTime;

        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        

        if(ShootTime > TimeToShoot)
        {
            if (Leftshooting && isLeftArm)
            {
                for(int i = 0; i < bulletsToShoot; i++)
                {
                    float x = Random.Range(-spread, spread);
                    Instantiate(fireBullet, gunPoint.position, Quaternion.Euler(new Vector3(0, 0, angle + x)), bullets.transform)
                        .GetComponent<SimpleBulletScript>().Initialize(GunDamage);
                }

                ShootTime = 0;
            }
            if (Rightshooting && !isLeftArm)
            {

                for(int i = 0; i < bulletsToShoot; i++)
                {
                    float x = Random.Range(-spread, spread);
                    Instantiate(lightningBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + x)), bullets.transform)
                        .GetComponent<SimpleBulletScript>().Initialize(GunDamage);
                }

                ShootTime = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space) && !isLeftArm)
            {

                for (int i = 0; i < bulletsToShoot; i++)
                {
                    float x = Random.Range(-spread, spread);
                    Instantiate(fireBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + x)), bullets.transform)
                        .GetComponent<SimpleBulletScript>().Initialize(GunDamage);
                }

                ShootTime = 0;
            }
        }
    }
}
