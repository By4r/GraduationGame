using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Runtime.Managers
{
    public class SettingsManager : MonoBehaviour
    {
        public AudioMixer audioMixer;

        public TMP_Dropdown resolutionDropdown;

        private Resolution[] resolutions;

        private void Start()
        {
            resolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                //string option = resolutions[i].width + " x " + resolutions[i].height;

                string option = resolutions[i].width + " x " + resolutions[i].height + " @ " +
                                resolutions[i].refreshRate + "hz";

                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        
        public void SetVolume(float decimalVolume)
        {
            var dbVolume = Mathf.Log10(decimalVolume) * 20;
            if (decimalVolume == 0.0f)
            {
                dbVolume = -80.0f;
            }
            audioMixer.SetFloat("volume", dbVolume);
        }
        
        // public void SetVolume(float value)
        // {
        //     audioMixer.SetFloat("volume", Mathf.Log10(value) * 20); 
        //     
        //     //audioMixer.SetFloat("volume", value);
        //
        //
        //     Debug.Log("Current Volume: " + value);
        // }

        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);

            Debug.Log("Current Quality Index: " + index);
        }

        public void SetFullscreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;

            Debug.Log("Fullscreen: " + isFullScreen);
        }
    }
}