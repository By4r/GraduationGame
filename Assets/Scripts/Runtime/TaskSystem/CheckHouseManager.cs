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
        
    }
}