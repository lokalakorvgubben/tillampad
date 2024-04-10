using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{
    private Vector3 originalScale;
    private void Start()
    {
        originalScale = transform.localScale;
    }
    public void PointerEnter()
    {
        transform.localScale = new Vector3(originalScale.x * 1.2f, originalScale.y * 1.2f, originalScale.z * 1.2f);
    }

    public void PointerExit()
    {
        transform.localScale = originalScale;
    }
}
