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
    }
}