using UnityEngine;

[ExecuteAlways]
public class DayManager : MonoBehaviour
{
    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    private bool _cycleActive;

    private void Update()
    {
        if (Preset == null)
            return;

        if (_cycleActive)
        {
            TimeOfDay += Time.deltaTime; 
            TimeOfDay %= 24; 
            UpdateLighting(TimeOfDay / 24f);


            // if (TimeOfDay >= 2f)
            // {
            //     _cycleActive = false;
            // }
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
}
