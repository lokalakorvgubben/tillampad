using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeed;
    public SpriteRenderer sr;
    public LayerMask groundLayer;

    float verticalInput;
    float horizontalInput;
    private bool paused = false;
    
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (!paused)
        {
            if (horizontalInput == -1)
            {
                sr.flipX = true;
            }
            else if (horizontalInput == 1)
            {
                sr.flipX = false;
            }
        }


        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        Vector2 moveSpeed = moveDirection * Time.deltaTime * playerSpeed;

        Vector2 newPosition = (Vector2)transform.position + moveSpeed;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 0.1f, groundLayer);
        if (colliders.Length > 0)
        {
            transform.Translate(moveSpeed);
        }
    }


}
