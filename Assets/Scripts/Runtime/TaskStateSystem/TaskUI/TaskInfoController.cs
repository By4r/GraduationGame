using TMPro;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Runtime.TaskStateSystem.TaskUI
{
    public class TaskInfoController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textInfo;
        [SerializeField] private Transform textInfoTab;

        #region Animation Variables

        [SerializeField] private Transform startPositionTransform;
        [SerializeField] private Transform endPositionTransform;
        [SerializeField] private float animationDuration = 1f;
        [SerializeField] private float animationDelay = 0.5f;

        #endregion

        internal void TaskInfo(string taskText)
        {
            textInfo.text = taskText;
        }
        
        [Button("Show Task Info!")]
        internal void ShowTaskInfo()
        {
            textInfoTab.position = startPositionTransform.position;
            textInfoTab.DOMove(endPositionTransform.position, animationDuration)
                .SetDelay(animationDelay)
                .SetEase(Ease.InOutQuad);
        }
        
        [Button("HIDE Task Info!")]
        internal void HideTaskInfo()
        {
            textInfoTab.position = endPositionTransform.position;
            textInfoTab.DOMove(startPositionTransform.position, animationDuration)
                .SetEase(Ease.InOutQuad);
                //.SetDelay(animationDelay)
        }
        
    }
}