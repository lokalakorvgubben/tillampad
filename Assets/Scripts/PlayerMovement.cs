using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public SpriteRenderer sr;
    public LayerMask groundLayer;

    private CircleCollider2D circleCollider;
    private bool paused = false;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (!paused)
        {
            Vector3 mouse_pos;
            Vector3 object_pos;

            mouse_pos = Input.mousePosition;
            object_pos = Camera.main.WorldToScreenPoint(transform.position);

            if (mouse_pos.x < object_pos.x)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }

        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        Vector2 moveSpeed = moveDirection * Time.deltaTime * playerSpeed;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, circleCollider.radius, moveDirection, moveSpeed.magnitude, groundLayer);

        if (hit.collider != null)
        {
            Vector2 slideDirection = Vector2.Reflect(moveDirection, hit.normal);
            moveSpeed = slideDirection * Time.deltaTime * playerSpeed;
        }

        transform.Translate(moveSpeed);
    }
}
