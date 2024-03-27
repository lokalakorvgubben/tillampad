using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeepingAngels : MonoBehaviour
{
    private EnemyScript enemy;
    public bool InSight = false;

    private void Start()
    {
        enemy = GetComponent<EnemyScript>();
    }

    private void Update()
    {
        if(InSight == true)
        {
            enemy.InSight = true;
        }
        else
        {
            enemy.InSight=false;
        }
    }
}