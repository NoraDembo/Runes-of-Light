using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFader : MonoBehaviour
{
    public float fadeSpeed;
    float targetIntensity = 0;
    Light light;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        light.intensity = Mathf.MoveTowards(light.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
    }

    public void SetIntensity(float f)
    {
        targetIntensity = f;
    }
}
