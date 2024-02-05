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

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {

        lineRenderer.SetPosition(1, target.position);
        lineRenderer.SetPosition(0, startEnemy.position);


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
