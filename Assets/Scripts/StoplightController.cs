using System.Collections;
using UnityEditor;
using UnityEngine;

public class StoplightController : MonoBehaviour
{
    [SerializeField] private Stoplight[] stoplights;

    public float redDuration = 4f;
    public float yellowDuration = 1.5f;
    public float greenDuration = 4f;

    void Start()
    {
        StartCoroutine(CycleLights());
    }

    private IEnumerator CycleLights()
    {
        while(true)
        {
            foreach(Stoplight light in stoplights) light.TurnRedOn();
            yield return new WaitForSeconds(redDuration);

            foreach(Stoplight light in stoplights) light.TurnGreenOn();
            yield return new WaitForSeconds(greenDuration);

            foreach(Stoplight light in stoplights) light.TurnYellowOn();
            yield return new WaitForSeconds(yellowDuration);
        }
        
    }
}
