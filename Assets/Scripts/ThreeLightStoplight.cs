using UnityEngine;

public class ThreeLightStoplight : Stoplight
{
    [SerializeField] private Renderer redRenderer; 
    [SerializeField] private Renderer yellowRenderer;
    [SerializeField] private Renderer greenRenderer;

    private Coroutine redFadeCoroutine;
    private Coroutine yellowFadeCoroutine;
    private Coroutine greenFadeCoroutine;

    protected override void Awake()
    {
        base.Awake();
        TurnAllOff();
    }

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

    // Turn off all of the lights
    public override void TurnAllOff()
    {
        StartFade(redRenderer, Color.red, offIntensity, fadeDuration, ref redFadeCoroutine);
        StartFade(yellowRenderer, Color.yellow, offIntensity, fadeDuration, ref yellowFadeCoroutine);
        StartFade(greenRenderer, Color.green, offIntensity, fadeDuration, ref greenFadeCoroutine);
    }
}
