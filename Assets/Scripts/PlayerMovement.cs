using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float XP = 0;
    public float maxXp;
    public int level = 1;
    public float playerSpeed;
    public SpriteRenderer sr;
    public LayerMask groundLayer;
    private ExperienceBar xpbar;
    private TextMeshProUGUI levelstext;

    float verticalInput;
    float horizontalInput;

    private void Start()
    {
        levelstext = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        xpbar = FindAnyObjectByType<ExperienceBar>();
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        xpbar.SetMaxExperience(maxXp);
        xpbar.setExperience(XP);
        levelstext.text = "LV." + level;

        if(XP >= maxXp)
        {
            if(XP > maxXp)
            {
                XP -= maxXp;
            }
            else
            {
                XP = 0;
            }
            level++;
            maxXp *= 1.5f;
        }

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
        XP += xpAmount;
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
