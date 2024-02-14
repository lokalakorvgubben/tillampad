using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController2 : MonoBehaviour
{
    // Start is called before the first frame update

    private LineRenderer lineRenderer;

    [SerializeField]
    private Texture[] textures;

    private int animationStep;

    [SerializeField]
    private float fps = 30f;

    private float fpsCounter;

    private Transform target;
    private Transform startEnemy;

    public float timer = 0;
    public float dieTimer = 1;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Die()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > dieTimer)
        {
            Die();
        }
        if(target != null)
        {
            lineRenderer.SetPosition(1, target.position);
        }
        else 
        {
            Die();
        }
        if(startEnemy != null) {
            lineRenderer.SetPosition(0, startEnemy.position);
        }
        else
        {
            Die();
        }
        


        fpsCounter += Time.deltaTime;
        if (fpsCounter >= 1f / fps)
        {
            animationStep++;

            if (animationStep == textures.Length)
            {
                animationStep = 0;
            }

            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0f;
        }
    }
    public void AssignTarget(Transform startEnemy, Transform newTarget)
    {
        this.startEnemy = startEnemy;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startEnemy.position);
        target = newTarget;
    }
}
