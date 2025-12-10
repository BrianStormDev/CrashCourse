using System.Collections;
using UnityEngine;

public abstract class Stoplight : MonoBehaviour
{
    protected float offIntensity = 0f;
    protected float onIntensity = 5f;
    protected float fadeDuration = 0.1f;

    private MaterialPropertyBlock block;

    protected virtual void Awake()
    {
        block = new MaterialPropertyBlock();
    }

    // Generic coroutine to fade a light to a target intensity
    protected IEnumerator FadeEmission(Renderer r, Color color, float targetIntensity, float duration)
    {
        r.GetPropertyBlock(block);
        float currentIntensity = block.GetColor("_EmissionColor").maxColorComponent;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newIntensity = Mathf.Lerp(currentIntensity, targetIntensity, elapsed / duration);
            block.SetColor("_EmissionColor", color * newIntensity);
            r.SetPropertyBlock(block);
            yield return null;
        }

        // Ensure the target intensity is reached
        block.SetColor("_EmissionColor", color * targetIntensity);
        r.SetPropertyBlock(block);
    }

    // Function that starts fade coroutine
    protected void StartFade(Renderer r, Color color, float targetIntensity, float duration, ref Coroutine coroutineRef)
    {
        if (coroutineRef != null)
        {
            StopCoroutine(coroutineRef);
        }
        coroutineRef = StartCoroutine(FadeEmission(r, color, targetIntensity, duration));
    }

    public abstract void TurnAllOff();
}
