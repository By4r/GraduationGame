using System.Collections;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Beast
{
    public class BeastPhysicsController : MonoBehaviour
    {
        private readonly string _player = "Player";
        [SerializeField] private BeastController beastController;
        [SerializeField] private GameObject jumpscarePrefab;
        [SerializeField] private AudioSource beastAudioSource;
        [SerializeField] private AudioClip jumpscareSound;
        [SerializeField] private AudioClip laughSound;
        
        /*private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_player))
            { 
                beastController.Jumpscare(jumpscarePrefab);
                Debug.Log("JUMPSCARE");
                beastAudioSource.PlayOneShot(jumpscareSound);
                beastAudioSource.PlayOneShot(laughSound);
                StartCoroutine(ShowFailedPanelWithDelay());
            }
        }

        private IEnumerator ShowFailedPanelWithDelay()
        {
            // Wait for a specified delay before showing the failed panel
            yield return new WaitForSeconds(2f); // Adjust the delay as needed

            ShowFailedPanel();
        }

        private void ShowFailedPanel()
        {
            CoreGameSignals.Instance.onLevelFailed.Invoke();
        }*/
    }
}