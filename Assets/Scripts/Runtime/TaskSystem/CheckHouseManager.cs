using UnityEngine;

namespace Runtime.TaskSystem
{
    public class CheckHouseManager : MonoBehaviour
    {
        [SerializeField] private GameObject paranormalGameObject;
        
        internal void ShowParanormal()
        {
            paranormalGameObject.SetActive(true);
        }

        internal void HideParanormal()
        {
            paranormalGameObject.SetActive(false);
        }
        
    }
}