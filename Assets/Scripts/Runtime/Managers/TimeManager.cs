using System;
using System.Collections;
using Runtime.Signals;
using Runtime.SoundSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private Texture2D skyboxNight;
        [SerializeField] private Texture2D skyboxSunrise;
        [SerializeField] private Texture2D skyboxDay;
        [SerializeField] private Texture2D skyboxSunset;
 
        [SerializeField] private Gradient graddientNightToSunrise;
        [SerializeField] private Gradient graddientSunriseToDay;
        [SerializeField] private Gradient graddientDayToSunset;
        [SerializeField] private Gradient graddientSunsetToNight;
 
        [SerializeField] private Light globalLight;
 
        private int minutes;
 
        public int Minutes
        { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }
 
        [SerializeField] private int hours; // Default 5
 
        public int Hours
        { get { return hours; } set { hours = value; OnHoursChange(value); } }
 
        private int days;
 
        public int Days
        { get { return days; } set { days = value; } }
 
        private float tempSecond;

        
        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        
        private void Start()
        {
            if (hours >= 6 && hours < 22)
            {
                AudioManager.Instance.PlayEnvironmentSound("DaySound");
            }
            else
            {
                AudioManager.Instance.PlayEnvironmentSound("NightSound");
            }
        }

        private void SubscribeEvents()
        {
            TimeSignals.Instance.onSetHours += OnSetHours;
        }

        private void OnSetHours(int value)
        {
            Hours = value;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            TimeSignals.Instance.onSetHours -= OnSetHours;
        }

        public void Update()
        {
            //tempSecond += Time.deltaTime;
 
            if (tempSecond >= 1)
            {
                Minutes += 10; // Normally +1
                tempSecond = 0;
            }
        }
 
        private void OnMinutesChange(int value)
        {
            globalLight.transform.Rotate(Vector3.up, (1f / (1440f / 4f)) * 360f, Space.World);
            //globalLight.transform.Rotate(Vector3.up, (1f / (1440f)) * 360f, Space.World);

            if (value >= 60)
            {
                Hours++;
                minutes = 0;
            }
            if (Hours >= 24)
            {
                Hours = 0;
                Days++;
            }
        }
        
        private void OnHoursChange(int value)
        {
            if (value == 6)
            {
                RenderSettings.fog = false;
                AudioManager.Instance.PlayEnvironmentSound("DaySound");
                StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, 10f));
                StartCoroutine(LerpLight(graddientNightToSunrise, 10f));
            }
            else if (value == 8)
            {
                StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 10f));
                StartCoroutine(LerpLight(graddientSunriseToDay, 10f));
            }
            else if (value == 18)
            {
                StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 10f));
                StartCoroutine(LerpLight(graddientDayToSunset, 10f));
            }
            else if (value == 22)
            {
                RenderSettings.fog = true;
                AudioManager.Instance.PlayEnvironmentSound("NightSound");
                StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 10f));
                StartCoroutine(LerpLight(graddientSunsetToNight, 10f));
            }
        }
 
        private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
        {
            RenderSettings.skybox.SetTexture("_Texture1", a);
            RenderSettings.skybox.SetTexture("_Texture2", b);
            RenderSettings.skybox.SetFloat("_Blend", 0);
            for (float i = 0; i < time; i += Time.deltaTime)
            {
                RenderSettings.skybox.SetFloat("_Blend", i / time);
                yield return null;
            }
            RenderSettings.skybox.SetTexture("_Texture1", b);
        }
 
        private IEnumerator LerpLight(Gradient lightGradient, float time)
        {
            for (float i = 0; i < time; i += Time.deltaTime)
            {
                globalLight.color = lightGradient.Evaluate(i / time);
                //RenderSettings.fogColor = globalLight.color;
                yield return null;
            }
        }
        
        
        
        [Button("TIME 5 ")]
        public void TimeFive()
        {
            //OnHoursChange(5);

            Hours = 5;
        }
        

        [Button("TIME 8 ")]
        public void TimeEight()
        {
            //OnHoursChange(8);

            Hours = 8;
        }
        
        [Button("TIME 18 ")]
        public void TimeEighteen()
        {
            //OnHoursChange(18);

            Hours = 18;
        }
        
        [Button("TIME 22 ")]
        public void TimeTwentyTwo()
        {
            //OnHoursChange(22);
            
            Hours = 22;
        }
        
    }
}