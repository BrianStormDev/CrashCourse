using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Stoplight : MonoBehaviour
{
    [SerializeField] private Renderer redRenderer; 
    [SerializeField] private Renderer yellowRenderer;
    [SerializeField] private Renderer greenRenderer;

    public float offIntensity = 0f;
    public float onIntensity = 5f;
    public float fadeDuration = 0.1f;

    private MaterialPropertyBlock block;

    private Coroutine redFadeCoroutine;
    private Coroutine yellowFadeCoroutine;
    private Coroutine greenFadeCoroutine;

    void Awake()
    {
        block = new MaterialPropertyBlock();
        TurnAllOff();
    }

    // Generic function to fade a light to a target intensirt
    private IEnumerator FadeEmission(Renderer r, Color color, float targetIntensity, float duration)
    {
        r.GetPropertyBlock(block);
        float currentIntensity = block.GetColor("_EmissionColor").maxColorComponent;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
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

    private void StartFade(Renderer r, Color color, float targetIntensity, float duration, ref Coroutine coroutineRef)
    {
        if (coroutineRef != null)
        {
            StopCoroutine(coroutineRef);
        }
        coroutineRef = StartCoroutine(FadeEmission(r, color, targetIntensity, fadeDuration));
    }

    // // Set the emission value of the renderer
    // void SetEmission(Renderer r, Color color, float intensity)
    // {
    //     r.GetPropertyBlock(block);
    //     block.SetColor("_EmissionColor", color * intensity);
    //     r.SetPropertyBlock(block);
    // }

    // Public API to turn lights on and off using fades

    // Turn on the Red Light
    public void TurnRedOn()
    {
        StartFade(redRenderer, Color.red, onIntensity, fadeDuration, ref redFadeCoroutine);
        StartFade(yellowRenderer, Color.yellow, offIntensity, fadeDuration, ref yellowFadeCoroutine);
        StartFade(greenRenderer, Color.green, offIntensity, fadeDuration, ref greenFadeCoroutine);
    }

    // Turn on the Yellow Light
    public void TurnYellowOn()
    {
        StartFade(redRenderer, Color.red, offIntensity, fadeDuration, ref redFadeCoroutine);
        StartFade(yellowRenderer, Color.yellow, onIntensity, fadeDuration, ref yellowFadeCoroutine);
        StartFade(greenRenderer, Color.green, offIntensity, fadeDuration, ref greenFadeCoroutine);
    }

    // Turn on the Green Light
    public void TurnGreenOn()
    {
        StartFade(redRenderer, Color.red, offIntensity, fadeDuration, ref redFadeCoroutine);
        StartFade(yellowRenderer, Color.yellow, offIntensity, fadeDuration, ref yellowFadeCoroutine);
        StartFade(greenRenderer, Color.green, onIntensity, fadeDuration, ref greenFadeCoroutine);
    }

    // Turn of all of the lights
    public void TurnAllOff()
    {
        StartFade(redRenderer, Color.red, offIntensity, fadeDuration, ref redFadeCoroutine);
        StartFade(yellowRenderer, Color.yellow, offIntensity, fadeDuration, ref yellowFadeCoroutine);
        StartFade(greenRenderer, Color.green, offIntensity, fadeDuration, ref greenFadeCoroutine);
    }
}
