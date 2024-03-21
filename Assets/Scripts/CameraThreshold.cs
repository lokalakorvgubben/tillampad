using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraThreshold : MonoBehaviour
{
    public Camera cam;
    public Transform player;
    public float threshold;

    private bool paused = false;

    void Update()
    {
        if(Time.timeScale == 0)
        {
            paused = true;
        }
        else
        {
            paused = false;
        }
        if (!paused)
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPos = (player.position + mousePos) / 2;

            targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
            targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

            this.transform.position = targetPos;
        }

    }
}
