using System.Collections;
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
            foreach(Stoplight light in stoplights) {
                if (light is ThreeLightStoplight tls)
                {
                    tls.TurnRedOn();
                }
            }
            yield return new WaitForSeconds(redDuration);

            foreach(Stoplight light in stoplights){
                if (light is ThreeLightStoplight tls)
                {
                    tls.TurnGreenOn();
                }
            };
            yield return new WaitForSeconds(greenDuration);

            foreach(Stoplight light in stoplights) {
                if (light is ThreeLightStoplight tls)
                {
                    tls.TurnYellowOn();
                }
            };
            yield return new WaitForSeconds(yellowDuration);
        }
    }
}
