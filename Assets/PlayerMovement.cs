using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementX = 0;
    public float movementY = 0;
    public Vector2 Movement;
    public float playerSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            movementX = -1;
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            movementX = 1;
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            movementX = 0;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementY = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementY = -1;
        }
        else
        {
            movementY = 0;
        }
        transform.Translate(new Vector2(movementX, movementY).normalized * Time.deltaTime * playerSpeed);
    }
}
