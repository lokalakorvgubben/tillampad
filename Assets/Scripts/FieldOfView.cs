using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class FieldOfView : MonoBehaviour
{
    public ArmScript armScript;
    public float radius = 5f;
    public LayerMask targetLayer;

    public GameObject Enemy;
    public bool CanSeeEnemy {  get; private set; }

    private void Start()
    {
        StartCoroutine(FOVCheck());
    }

    private void Update()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        if(Enemy == null)
        {
            Debug.Log("No Enemies");
        }
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FOV();
        }
    }

    private void FOV()
    {
        // Get the direction to the mouse
        Vector3 mouseDirection = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;

        // Calculate the angle between the up direction and the mouse direction
        float angleToMouse = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        // Adjust the FOV based on the angle to the mouse
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToMouse));

        // Perform the raycast as before to determine if an enemy is within the FOV
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mouseDirection, radius, targetLayer);

        if (hit.collider != null)
        {
            CanSeeEnemy = true;
            Enemy = hit.collider.gameObject;
        }
        else
        {
            CanSeeEnemy = false;
            Enemy = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -360 / 2);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, 360 / 2);

        Gizmos.color = Color.yellow;

        // Draw the FOV lines using the calculated angle to the mouse
        Vector3 mouseDirection = DirectionFromAngle(-transform.eulerAngles.z, 0);
        Gizmos.DrawLine(transform.position, transform.position + mouseDirection * radius);

        // Draw the FOV lines using the predefined angles
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if (CanSeeEnemy)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, Enemy.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}