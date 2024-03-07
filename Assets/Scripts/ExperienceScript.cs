using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceScript : MonoBehaviour
{


    public float xpAmount = 5;
    public float xpSpeed = 1;
    public float xpDistToMove = 5;
    private BoxCollider2D hitbox;
    private GameObject player;
    private PlayerMovement playerScript;


    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.gameObject.GetComponent<PlayerMovement>();
        Debug.Log(hitbox);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log(dist);
        if(dist < xpDistToMove)
        {
            float step = (xpSpeed * Time.deltaTime) / dist;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
        }
    }
    public void pickUp()
    {

        playerScript.GainXP(xpAmount);


        Destroy(gameObject);
    }
}
