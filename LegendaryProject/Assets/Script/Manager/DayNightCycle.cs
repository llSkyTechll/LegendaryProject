using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public float secondsInFullDay = 120f;
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    public float timeMultiplier = 10f;
    float sunInitialIntensify;
    public float startIntensitymultiplier = 0.35f;
    public float vitesseLeverCoucherSoleil = 4f;
    private GameObject cam;
    private Skybox sky;
    public Material day;
    public Material night;
    public float dayStart = 0.15f;
    public float dayEnd = 0.85f;
    // Use this for initialization
    void Start()
    {
        sunInitialIntensify = sun.intensity;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        sky = cam.GetComponent<Skybox>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateSun();

        UpdateTimeOfDay();
    }

    void UpdateTimeOfDay()
    {
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 180f)-1, 0, 0);
        float intensityMultiplier = 1;
        sun.shadowStrength = 0.5f;
        sky.material = day;

        if (currentTimeOfDay <= dayStart || currentTimeOfDay >= dayEnd)
        {
            sky.material = night;
            intensityMultiplier = startIntensitymultiplier;

        }
        else if (currentTimeOfDay <= 0.30f)
        {
            intensityMultiplier = startIntensitymultiplier + (Mathf.Clamp01(currentTimeOfDay - 0.14f) * vitesseLeverCoucherSoleil);
            if (intensityMultiplier >= 1f)
            {
                intensityMultiplier = 1f;
            }
        }
        else if (currentTimeOfDay >= 0.70f)
        {
            intensityMultiplier = 1 - (Mathf.Clamp01(currentTimeOfDay - 0.69f) * vitesseLeverCoucherSoleil);
            if (intensityMultiplier <= startIntensitymultiplier)
            {
                intensityMultiplier = startIntensitymultiplier;
            }
        }
        sun.intensity = sunInitialIntensify * intensityMultiplier;
        setShadowOff(intensityMultiplier);
    }

    void setShadowOff(float inIntensityMultiplier)
    {
        if (inIntensityMultiplier == startIntensitymultiplier)
        {
            sun.shadowStrength = 0;
        }
        else
        {
            sun.shadowStrength = inIntensityMultiplier / 4;
        }
    }

}
