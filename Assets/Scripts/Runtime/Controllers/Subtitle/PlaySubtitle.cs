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
>>>>>>> 28365731b4383292618be3fe59e77be0725dce7f

            for (int i = 0; i < sub.Length; i++)
            {
                _textController.SetText(sub[i]);
                yield return new WaitForSeconds(durations[i]);
            }

            _textController.ClearText();
        }
    }
}