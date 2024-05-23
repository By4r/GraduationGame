using System;
using DG.Tweening;
using Runtime.Controllers.Player;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.Item_Inventory
{
    public class AtticStairsController : MonoBehaviour
    {
        [SerializeField] private GameObject blackPanel;
        [SerializeField] private Image blackImage;
        [SerializeField] private PlayerPhysicsController _playerPhysicsController;
        [SerializeField] private Transform atticPosition;
        [SerializeField] private Transform floorPosition;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private float transitionDuration = 1f; // Duration for position transition

        private void Update()
        {
            Ray raycast = _playerPhysicsController.GetRaycast();
            float range = _playerPhysicsController.range;

            if (Physics.Raycast(raycast, out RaycastHit hit, range))
            {
                if (hit.collider.CompareTag("AtticStairsUP"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        PanelAnimation(() => TransformToPosition(atticPosition));
                    }
                }
                else if (hit.collider.CompareTag("AtticStairsDOWN"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        PanelAnimation(() => TransformToPosition(floorPosition));
                    }
                }
            }
        }

        private void PanelAnimation(Action onComplete)
        {
            blackPanel.SetActive(true);
            blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, 0f);

            blackImage.DOFade(1f, 1f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    onComplete.Invoke();
                    DOVirtual.DelayedCall(1f, () =>
                    {
                        blackImage.DOFade(0f, 1f)
                            .SetEase(Ease.InOutQuad)
                            .OnComplete(() =>
                            {
                                blackPanel.SetActive(false);
                                Debug.Log("Attic transition DONE!");
                            });
                    });
                });
        }

        private void TransformToPosition(Transform targetPosition)
        {
            _playerManager.transform.DOMove(targetPosition.position, transitionDuration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    Debug.Log("Player moved to target position");
                });
        }
    }
}
