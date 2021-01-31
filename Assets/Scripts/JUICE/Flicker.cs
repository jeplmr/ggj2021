using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    private Light _light;
    private float _ogBrightness; 
    [Range(0.01f, 0.5f)]
    public float flickerVariance; 
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        _ogBrightness = _light.intensity; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _light.intensity = Random.Range(_ogBrightness - flickerVariance, _ogBrightness + flickerVariance); 
    }
}
