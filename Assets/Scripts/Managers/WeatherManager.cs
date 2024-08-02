using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public ParticleSystem rain;
    
    public void Rain()
    {
        rain.Play();
    }
    public void RainStop()
    {
        rain.Stop(); 
    }
}
