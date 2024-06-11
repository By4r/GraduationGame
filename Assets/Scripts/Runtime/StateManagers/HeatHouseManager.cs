using System;
using System.Collections;
using Runtime.SoundSystem;
using UnityEngine;

namespace Runtime.TaskSystem
{
    public class HeatHouseManager : MonoBehaviour
    {
        [SerializeField] private GameObject paranormalGameObject;
        [SerializeField] internal GameObject secondLetter;

        internal void ShowParanormal()
        {
            paranormalGameObject.SetActive(true);
            StartCoroutine(HideParanormalAfterDelay(5f)); 
        }
        
        internal void HideParanormal()
        {
            AudioManager.Instance.StopStateSound();
            paranormalGameObject.SetActive(false);
        }
        
        IEnumerator HideParanormalAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            HideParanormal();
        }
    }
}