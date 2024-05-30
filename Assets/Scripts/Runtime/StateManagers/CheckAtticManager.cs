using UnityEngine;

namespace Runtime.StateManagers
{
    public class CheckAtticManager:MonoBehaviour
    {
        [SerializeField] private GameObject _atticCamScareTrigger;

        internal void ActiveTrigger()
        {
            _atticCamScareTrigger.SetActive(true);
        }

        internal void DeActiveTrigger()
        {
            _atticCamScareTrigger.SetActive(false);
        }
    }
}