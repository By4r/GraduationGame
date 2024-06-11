using Runtime.Controllers.Subtitle;
using UnityEngine;

namespace Runtime.StateManagers
{
    public class CheckAtticManager:MonoBehaviour
    {
        [SerializeField] private GameObject _atticCamScareTrigger;
        [SerializeField] private GameObject firstLetter;

        internal void ShowLetter()
        {
            firstLetter.SetActive(true);
            PlaySubtitle.Instance.PlayAudioWithSubtitle("want_home");
        }
        internal void CloseFirstLetter()
        {
            firstLetter.SetActive(false);
        }
        
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