using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Beast
{
    public class BeastAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BeastSignals.Instance.onChangeBeastAnimationState += OnChangeAnimationState;
        }

        private void UnsubscribeEvents()
        {
            BeastSignals.Instance.onChangeBeastAnimationState -= OnChangeAnimationState;
        }

        private void OnChangeAnimationState(BeastAnimationStates animationState)
        {
            SetAnimationState(animationState);
        }

        internal void OnReset()
        {
            BeastSignals.Instance.onChangeBeastAnimationState?.Invoke(BeastAnimationStates.Idle);
        }

        private void SetAnimationState(BeastAnimationStates animationState)
        {
            animator.SetTrigger(animationState.ToString());
        }
    }
}