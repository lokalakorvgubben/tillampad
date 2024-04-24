using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class GunStats : MonoBehaviour
{
    public float detectionRadius = 5f;
    private GameObject gunSelect;

    [Header("Gun Stats")]
    public float manaToShoot;
    [Range(0, 10)] public float TimeToShoot = 1;
    public float spread;
    public int bulletsToShoot = 1;
    public float damage = 1;
    public float bulletspeed = 5;
    public Sprite gunSprite;
    private GameObject InteractUI;
    public Vector3 pivot;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = gunSprite;
        InteractUI = transform.Find("Interact").gameObject;
        InteractUI.SetActive(false);
        gunSelect = FindInActiveObjectByName("WeaponSelect");
    }
    private void Update()
    {
        CheckForPlayer();
    }

    private bool isPlayerDetected = false;

    private void CheckForPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + pivot, detectionRadius);
        bool playerFound = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.GetComponent<PlayerMovement>())
            {
                float distanceToPlayer = Vector2.Distance(transform.position, collider.transform.position);
                if (distanceToPlayer <= detectionRadius)
                {
                    playerFound = true;
                    isPlayerDetected = true;
                    InteractUI.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        gunSelect.SetActive(true);
                        var stats = gunSelect.GetComponent<StoreGunStats>();

                        stats.damage = damage;
                        stats.bulletspeed = bulletspeed;
                        stats.bulletsToShoot = bulletsToShoot;
                        stats.manaToShoot = manaToShoot;
                        stats.spread = spread;
                        stats.gunSprite = gunSprite;
                        stats.TimeToShoot = TimeToShoot;

                        Destroy(gameObject);
                        return;
                    }
                }
            }
        }

        if (!playerFound && isPlayerDetected)
        {
            InteractUI.SetActive(false);
            isPlayerDetected = false;
        }
    }



    GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + pivot, detectionRadius);
    }
}
