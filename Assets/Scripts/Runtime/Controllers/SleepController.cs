using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Runtime.Signals;
using Runtime.TaskStateSystem;
using Runtime.TaskStateSystem.TaskStates;

namespace Runtime.Controllers
{
    public class SleepController : MonoBehaviour
    {
        [SerializeField] private GameObject sleepPanel;
        
        public Transform Outbuilding;

        private Image sleepImage;

        private void Start()
        {
            sleepImage = sleepPanel.GetComponent<Image>();
        }

        internal void Sleep(TaskStateManager stateManager)
        {
            sleepPanel.SetActive(true);

            sleepImage.color = new Color(sleepImage.color.r, sleepImage.color.g, sleepImage.color.b, 0f);

            sleepImage.DOFade(1f, 1f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(1f, () =>
                    {
                        sleepImage.DOFade(0f, 1f)
                            .SetEase(Ease.InOutQuad)
                            .OnComplete(() =>
                            {
                                Debug.Log("SLEEPING DONE!");

                                stateManager.SetState(new CheckHouseState());
                            });
                    });
                });
        }

        internal void SleepCompulsory(TaskStateManager stateManager)
        {
            sleepPanel.SetActive(true);

            sleepImage.color = new Color(sleepImage.color.r, sleepImage.color.g, sleepImage.color.b, 0f);

            sleepImage.DOFade(1f, 1f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(0.6f, () =>
                    {
                        sleepImage.DOFade(0f, 2f)
                            .SetEase(Ease.InOutQuad)
                            .OnComplete(() =>
                            {
                                Debug.Log("SLEEPING DONE!");
                            });
                    });
                });
        }
        
    }
}