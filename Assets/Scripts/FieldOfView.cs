using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public ArmScript armScript;

    [Header("Field of View Settings")]
    public float baseRadius = 5f; // Default radius
    public float radius { get; private set; } // Actual radius

    public LayerMask targetLayer;

    public List<WeepingAngel> DetectedEnemies { get; private set; } = new List<WeepingAngel>();
    public bool CanSeeEnemy { get { return DetectedEnemies.Count > 0; } }

    private void Start()
    {
        radius = baseRadius; // Initialize radius
    }

    private void Update()
    {
        if (DetectedEnemies.Count == 0)
        {
            Debug.Log("No Enemies");
        }
        FOV();
    }

    private void FOV()
    {
        // Get the direction to the mouse
        Vector3 mouseDirection = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;

        // Calculate the angle between the up direction and the mouse direction
        float angleToMouse = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        // Adjust the FOV based on the angle to the mouse
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angleToMouse + 90));

        // Perform the raycast using the targetLayer
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, mouseDirection, radius, targetLayer);

        // Reset InSight to false for all enemies before checking the raycast hits
        foreach (WeepingAngel enemy in DetectedEnemies)
        {
            enemy.InSight = false;
        }

        // Clear the list of detected enemies
        DetectedEnemies.Clear();

        foreach (RaycastHit2D hit in hits)
        {
            // Calculate the direction to the enemy
            Vector2 directionToEnemy = (hit.transform.position - transform.position).normalized;

            // Check if the enemy is within the FOV range (angle) and within the specified radius
            float angleToEnemy = Vector2.Angle(transform.up, directionToEnemy);
            float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

            if (angleToEnemy < 360 / 2 && distanceToEnemy <= radius)
            {
                WeepingAngel enemy = hit.collider.gameObject.GetComponent<WeepingAngel>();
                if (enemy != null)
                {
                    enemy.InSight = true;
                    DetectedEnemies.Add(enemy);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -360 / 2);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, 360 / 2);

        Gizmos.color = Color.yellow;

        // Draw the FOV lines using the calculated angle to the mouse
        Vector3 mouseDirection = DirectionFromAngle(-transform.eulerAngles.z, 0);
        Gizmos.DrawLine(transform.position, transform.position + mouseDirection * radius);

        // Draw the FOV lines using the predefined angles
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        // Draw lines to detected enemies
        Gizmos.color = Color.green;
        foreach (WeepingAngel enemy in DetectedEnemies)
        {
            Gizmos.DrawLine(transform.position, enemy.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }
}
