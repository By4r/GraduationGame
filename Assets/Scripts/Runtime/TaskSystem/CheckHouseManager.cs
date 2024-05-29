using System;
using System.Collections;
using Runtime.SoundSystem;
using UnityEngine;

namespace Runtime.TaskSystem
{
    public class CheckHouseManager : MonoBehaviour
    {
        [SerializeField] private GameObject paranormalGameObject;
        [SerializeField] private GameObject paranormalTriggerEnter;
        [SerializeField] private GameObject paranormalTriggerExit;
        

        internal void ShowParanormal()
        {
            AudioManager.Instance.PlayStateSounds("KnockingWindowSound");

            paranormalGameObject.SetActive(true);
            StartCoroutine(HideParanormalAfterDelay(2.5f)); 
        }

        internal void HideParanormal()
        {
            AudioManager.Instance.StopStateSound();
            paranormalGameObject.SetActive(false);
        }

        internal void ActiveTriggers()
        {
            paranormalTriggerEnter.SetActive(true);
            paranormalTriggerExit.SetActive(true);
        }
        
        internal void DeActiveTriggers()
        {
            paranormalTriggerEnter.SetActive(false);
            paranormalTriggerExit.SetActive(false);
        }

        internal void ActiveExitTrigger()
        {
            paranormalTriggerExit.SetActive(true);
        }
        
        
        IEnumerator HideParanormalAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            HideParanormal();
        }
    }
}