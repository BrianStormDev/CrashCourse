using System.Collections;
using UnityEditor;
using UnityEngine;

public class StoplightController : MonoBehaviour
{
    public Renderer redRenderer; 
    public Renderer yellowRenderer;
    public Renderer greenRenderer;

    public float offIntensity = 0f;
    public float onIntensity = 5f;

    private MaterialPropertyBlock block;

    void Awake()
    {
        block = new MaterialPropertyBlock();
        TurnAllOff();
    }

    IEnumerator Start()
    {
        // Automatic cycling
        while(true)
        {
            TurnRedOn();
            yield return new WaitForSeconds(4f);

            TurnGreenOn();
            yield return new WaitForSeconds(4f);

            TurnYellowOn();
            yield return new WaitForSeconds(1.5f);
        }
    }

    void SetEmission(Renderer r, Color color, float intensity)
    {
        r.GetPropertyBlock(block);
        block.SetColor("_EmissionColor", color * intensity);
        r.SetPropertyBlock(block);
    }

    public void TurnRedOn()
    {
        TurnAllOff();
        SetEmission(redRenderer, Color.red, onIntensity);
    }

    public void TurnYellowOn()
    {
        TurnAllOff();
        SetEmission(yellowRenderer, Color.yellow, onIntensity);
    }

    public void TurnGreenOn()
    {
        TurnAllOff();
        SetEmission(greenRenderer, Color.green, onIntensity);
    }

    public void TurnAllOff()
    {
        SetEmission(redRenderer, Color.red, offIntensity);
        SetEmission(yellowRenderer, Color.yellow, offIntensity);
        SetEmission(greenRenderer, Color.green, offIntensity);
    }


    // void Start()
    // {
        
    // }

    // void Update()
    // {
        
    // }
    
    // void Start ()
    // {
    //     renderer.material.color = color;
    // }

    // void Update ()
    // {
    //     f = getIntensity();
    //     Color color = Color.red * f;
    //     renderer.material.SetColor("_EmissionColor", color);
    // }
}
