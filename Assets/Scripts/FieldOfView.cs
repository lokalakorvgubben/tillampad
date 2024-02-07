using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius = 5f;
    [Range(1, 360)] public float angle = 90f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    
    public bool CanSeeEnemy {  get; private set; }

    private void Start()
    {
        StartCoroutine(FOVCheck());
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
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                    CanSeeEnemy = true;
                else
                    CanSeeEnemy = false;
            }
            CanSeeEnemy = false;
        }
        else if (CanSeeEnemy)
            CanSeeEnemy = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
    }
}
