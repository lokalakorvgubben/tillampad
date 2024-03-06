using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour
{
    public bool allowButtonHold;
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
    private bool shooting;


    public enum BulletType
    {
        Fire,
        Lightning,
        // Add more bullet types as needed
    }

    public BulletType currentBulletType = BulletType.Fire;

    // Start is called before the first frame update
    void Start()
    {
        bullets = GameObject.Find("Bullets");
        ShootTime = TimeToShoot;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLeftArm)
        {
            if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse1);
            else shooting = Input.GetKeyDown(KeyCode.Mouse1);
        }
        else if(isLeftArm)
        {
            if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

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
            if (shooting)
            {
                for(int i = 0; i < bulletsToShoot; i++)
                {
                    float x = Random.Range(-spread, spread);
                    GameObject bulletPrefab = GetBulletPrefab();
                    Instantiate(bulletPrefab, gunPoint.position, Quaternion.Euler(new Vector3(0, 0, angle + x)), bullets.transform)
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
    GameObject GetBulletPrefab()
    {
        switch (currentBulletType)
        {
            case BulletType.Fire:
                return fireBullet;
            case BulletType.Lightning:
                return lightningBullet;
            // Add more cases for additional bullet types
            default:
                return fireBullet; // Default to fireBullet if the type is not recognized
        }
    }
}
