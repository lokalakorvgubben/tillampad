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

    // Start is called before the first frame update
    void Start()
    {
        ShootTime = TimeToShoot;
    }

    // Update is called once per frame
    void Update()
    {
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && isLeftArm)
            {
                for(int i = 0; i < bulletsToShoot; i++)
                {
                    float x = Random.Range(-spread, spread);
                    Instantiate(fireBullet, gunPoint.position, Quaternion.Euler(new Vector3(0, 0, angle + x)))
                        .GetComponent<SimpleBulletScript>().Initialize(GunDamage);
                }

                ShootTime = 0;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && !isLeftArm)
            {

                for(int i = 0; i < bulletsToShoot; i++)
                {
                    float x = Random.Range(-spread, spread);
                    Instantiate(lightningBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + x)))
                        .GetComponent<SimpleBulletScript>().Initialize(GunDamage);
                }

                ShootTime = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space) && !isLeftArm)
            {

                for (int i = 0; i < bulletsToShoot; i++)
                {
                    float x = Random.Range(-spread, spread);
                    Instantiate(fireBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + x)))
                        .GetComponent<SimpleBulletScript>().Initialize(GunDamage);
                }

                ShootTime = 0;
            }
        }
    }
}
