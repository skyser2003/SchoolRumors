using UnityEngine;
using System.Collections;

public class FlickerLight : MonoBehaviour
{
    public float intensityVariation = 0.15f;
    float minIntensity;
    float maxIntensity;
    public float speed = 1.5f;
    Light fLight;

    float random;

    void Start()
    {
        fLight = GetComponent<Light>();
        minIntensity = fLight.intensity - intensityVariation;
        maxIntensity = fLight.intensity + intensityVariation;
        random = Random.Range(0.0f, 65535.0f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time * speed);
        fLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
