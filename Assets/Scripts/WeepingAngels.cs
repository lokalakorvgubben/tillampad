using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeepingAngels : MonoBehaviour
{
    public bool InSight;
    private EnemyScript main;

    private void Start()
    {
        main = GetComponent<EnemyScript>();
    }
    void Update()
    {
        if(InSight)
        {
            main.InSight = true;
        }
        else if(!InSight)
        {
            main.InSight = false;
        }
    }
}
