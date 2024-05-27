using System;
using UnityEngine;

namespace Runtime.TaskSystem
{
    public class CheckHouseManager : MonoBehaviour
    {
        [SerializeField] private GameObject paranormalGameObject;
        [SerializeField] private GameObject paranormalTriggerEnter;
        [SerializeField] private GameObject paranormalTriggerExit;


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            
        }

        internal void ShowParanormal()
        {
            paranormalGameObject.SetActive(true);
        }

        internal void HideParanormal()
        {
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
        
    }
}