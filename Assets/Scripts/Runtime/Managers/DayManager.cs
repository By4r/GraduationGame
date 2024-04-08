using System;
using UnityEngine;

[ExecuteAlways]
public class DayManager : MonoBehaviour
{
    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    private bool _cycleActive = true;
    private bool _isDaytime = false;

    [Header("Real Time(sec) per Game Hour")]
    [SerializeField] float realTime;
    
    float gameTimeHour; 

    private void Start()
    {
        CalculateGameTimeHour();
    }

    private void Update()
    {
        if (Preset == null)
            return;

        if (_cycleActive && Application.isPlaying)
        {

            TimeOfDay += Time.deltaTime;

            //TimeOfDay += Time.deltaTime / (60 * 60) * gameTimeHour;

            TimeOfDay %= 24;

            UpdateLighting(TimeOfDay / 24f);

            
            if ((int)TimeOfDay % 2 == 0 && _isDaytime)
            {
                _cycleActive = false;
                _isDaytime = false;
            }
            else if ((int)TimeOfDay % 2 != 0 && !_isDaytime)
            {
                _isDaytime = true;
            }
        }

       
        if (Input.GetKeyDown(KeyCode.N))
        {
            _cycleActive = !_cycleActive;
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    private void CalculateGameTimeHour()
    {
        gameTimeHour = realTime * 360f;
    }

    
    public void SetRealTime(float newRealTime)
    {
        realTime = newRealTime;
        CalculateGameTimeHour();
    }
}
