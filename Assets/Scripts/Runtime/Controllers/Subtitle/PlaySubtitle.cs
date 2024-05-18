using System.Collections;
using UnityEngine;

namespace Runtime.Controllers.Subtitle
{
    public class PlaySubtitle : MonoBehaviour
    {
        [SerializeField] private SubtitleManager _subtitleManager;
        [SerializeField] private TextController _textController;


        private void Start()
        {
            _textController.ClearText();
        }

        internal void PlaySpeech(AudioSource audioSource)
        {
            StartCoroutine(StartSubtitle(audioSource));
        }

        private IEnumerator StartSubtitle(AudioSource audioSource)
        {
<<<<<<< HEAD
            var (sub, durations)  = _subtitleManager.GetTextWithDurations(audioSource.clip.name);
=======
            var (sub, durations) = _subtitleManager.GetTextWithDurations(audioSource.clip.name);
            var lineDuration = audioSource.clip.length / sub.Length;
>>>>>>> 3a1d574d7b95b317706b0067fd2f8e1cc3213eae

            for (int i = 0; i < sub.Length; i++)
            {
                _textController.SetText(sub[i]);
                yield return new WaitForSeconds(durations[i]);
            }

            _textController.ClearText();
        }
    }
}