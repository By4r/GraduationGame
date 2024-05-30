using System.Collections;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace Runtime.TaskSystem
{
    public class CheckOfficeManager : MonoBehaviour
    {
        [SerializeField] private GameObject drawerObject;
        [SerializeField] private DOTweenAnimation drawerAnim;
        [SerializeField] private GameObject letterObject;

        private bool _drawerAnimationPlayed;

        public bool DrawerAnimationPlayed
        {
            get { return _drawerAnimationPlayed; }
            set { _drawerAnimationPlayed = value; }
        }

        internal void KeyReceived()
        {
            Debug.LogWarning("Key Received !");
        }

        internal void PlayDrawerAnimation()
        {
            Debug.Log("PLAY DRAWER");

            if (drawerAnim != null)
            {
                drawerAnim.DOPlay();

                StartCoroutine(WaitForDrawerAnimation());
            }
            else
            {
                Debug.LogWarning("Tween is not assigned!");
            }
        }

        private IEnumerator WaitForDrawerAnimation()
        {
            letterObject.SetActive(true);
            yield return new WaitWhile(() => drawerAnim.isActiveAndEnabled);

            //_drawerAnimationPlayed = true;
        }
    }
}