using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Runtime.Managers
{
    public class SettingsManager : MonoBehaviour
    {
        public AudioMixer audioMixer;
        
        public void SetVolume(float value)
        {
            audioMixer.SetFloat("volume",value);
            
            Debug.Log("Current Volume: " + value);
        }

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