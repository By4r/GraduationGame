using UnityEngine;

namespace Runtime.Managers
{
    public class CamScareManager:MonoBehaviour
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
    }
}