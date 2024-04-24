using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeOut : MonoBehaviour
{
    private SpriteRenderer sr;
    public float startDelay;
    public float fadeDuration;

    private void Start()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(FadeOut());

    }

    private IEnumerator FadeOut()
    {
        Color startColor = sr.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float lerpFactor = elapsedTime / fadeDuration;

            Color lerpedColor = Color.Lerp(startColor, endColor, lerpFactor);

            sr.color = lerpedColor;

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        sr.color = endColor;

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }

}
