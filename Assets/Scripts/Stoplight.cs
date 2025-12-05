using Unity.VisualScripting;
using UnityEngine;

public class Stoplight : MonoBehaviour
{
    [SerializeField] private Renderer redRenderer; 
    [SerializeField] private Renderer yellowRenderer;
    [SerializeField] private Renderer greenRenderer;

    public float offIntensity = 0f;
    public float onIntensity = 5f;

    private MaterialPropertyBlock block;

    void Awake()
    {
        block = new MaterialPropertyBlock();
        TurnAllOff();
    }

    // Set the emission value of the renderer
    void SetEmission(Renderer r, Color color, float intensity)
    {
        r.GetPropertyBlock(block);
        block.SetColor("_EmissionColor", color * intensity);
        r.SetPropertyBlock(block);
    }

    // Turn on the Red Light
    public void TurnRedOn()
    {
        TurnAllOff();
        SetEmission(redRenderer, Color.red, onIntensity);
    }

    // Turn on the Yellow Light
    public void TurnYellowOn()
    {
        TurnAllOff();
        SetEmission(yellowRenderer, Color.yellow, onIntensity);
    }

    // Turn on the Green Light
    public void TurnGreenOn()
    {
        TurnAllOff();
        SetEmission(greenRenderer, Color.green, onIntensity);
    }

    // Turn of all of the lights
    public void TurnAllOff()
    {
        SetEmission(redRenderer, Color.red, offIntensity);
        SetEmission(yellowRenderer, Color.yellow, offIntensity);
        SetEmission(greenRenderer, Color.green, offIntensity);
    }
}
