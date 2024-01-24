using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour
{

    public bool isLeftArm;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse_pos;
        Vector3 object_pos;
        float angle;
        mouse_pos = Input.mousePosition;
        object_pos = Camera.main.WorldToScreenPoint(transform.position);

        


        if (Input.GetKeyDown(KeyCode.Mouse0) && isLeftArm)
        {
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            Debug.Log(transform.rotation.z);

        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isLeftArm)
        {
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            Debug.Log(transform.rotation.z);
        }





    }
}
