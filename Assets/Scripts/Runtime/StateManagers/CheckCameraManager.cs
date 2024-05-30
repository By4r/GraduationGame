using System.Collections;
using Runtime.SoundSystem;
using UnityEngine;

namespace Runtime.StateManagers
{
    public class CheckCameraManager : MonoBehaviour
    {
        [SerializeField] private GameObject paranormalGameObject;

        
        internal void ShowParanormal()
        {
            paranormalGameObject.SetActive(true);
        }
        
        internal void ShowParanormalThenHide()
        {
            paranormalGameObject.SetActive(true);
            StartCoroutine(HideParanormalAfterDelay(5f)); 
        }

        private IEnumerator HideParanormalAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            HideParanormal();
        }

        internal void HideParanormal()
        {
            paranormalGameObject.SetActive(false);
        }
    }
}