using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    float verticalInput;
    float horizontalInput;
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(-horizontalInput, transform.localScale.y, transform.localScale.z);
        }

        Vector2 moveSpeed = new Vector2(horizontalInput, verticalInput).normalized * Time.deltaTime * playerSpeed;
        Debug.Log(moveSpeed.sqrMagnitude);
        transform.Translate(moveSpeed);
    }
}
