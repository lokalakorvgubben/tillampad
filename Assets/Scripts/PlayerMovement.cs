using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float totalXP = 0;
    public float playerSpeed;
    public SpriteRenderer sr;
    public LayerMask groundLayer;

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
        else if (horizontalInput == 1)
        {
            sr.flipX = false;
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
    public void GainXP(float xpAmount)
    {
        totalXP += xpAmount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        Debug.Log(collision.name);
        if (collision.gameObject.GetComponent<ExperienceScript>())
        {
            Debug.Log(collision.name + "william");
            var xp = collision.gameObject.GetComponent<ExperienceScript>();
            xp.pickUp();
        }
    }
}
