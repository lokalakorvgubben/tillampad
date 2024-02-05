using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public SpriteRenderer sr;
    float verticalInput;
    float horizontalInput;
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput == -1)
        {
            sr.flipX = true;
        }
        else if(horizontalInput == 1)
        {
            sr.flipX = false;
        }

        Vector2 moveSpeed = new Vector2(horizontalInput, verticalInput).normalized * Time.deltaTime * playerSpeed;
        //Debug.Log(moveSpeed.sqrMagnitude);
        transform.Translate(moveSpeed);
    }
}
