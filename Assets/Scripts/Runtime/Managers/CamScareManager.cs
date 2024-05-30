using UnityEngine;
using System.Collections;

namespace Runtime.Managers
{
    public class CamScareManager : MonoBehaviour
    {
        [SerializeField] private GameObject GhostObject;

        internal void showGhost()
        {
            GhostObject.SetActive(true);
        }

        internal void hideGhost()
        {
            GhostObject.SetActive(false);
        }

        internal void showGhostTemporary()
        {
            StartCoroutine(ShowGhostTemporarilyCoroutine());
        }

        private IEnumerator ShowGhostTemporarilyCoroutine()
        {
            GhostObject.SetActive(true);
            yield return new WaitForSeconds(5f); // Wait for 5 seconds
            hideGhost();
        }
    }
}