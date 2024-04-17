using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search.Providers;
using UnityEngine;

public class UIFollowPlayer : MonoBehaviour
{
    private GameObject player;
    private Camera cam;

    public Vector2 pivot;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(player.transform.position);

        Vector2 viewportPos = cam.ScreenToViewportPoint(screenPos);

        Vector2 canvasPos = new Vector2(viewportPos.x * GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.width,
                                        viewportPos.y * GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.height);

        transform.localPosition = canvasPos - GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.size / 2f + pivot;
    }
}
