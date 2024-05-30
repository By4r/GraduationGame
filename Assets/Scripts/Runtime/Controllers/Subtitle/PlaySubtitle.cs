using System.Collections;
using Runtime.SoundSystem;
using UnityEngine;

namespace Runtime.Controllers.Subtitle
{
    public class PlaySubtitle : MonoBehaviour
    {
        public static PlaySubtitle Instance;
        
        [SerializeField] private SubtitleManager _subtitleManager;
        [SerializeField] private TextController _textController;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            _textController.ClearText();
        }

        public void PlayAudioWithSubtitle(string audioClipName)
        {
            var audioManager = AudioManager.Instance;
            if (audioManager == null)
            {
                Debug.LogError("AudioManager instance not found!");
                return;
            }

            audioManager.PlayPlayerTalkingSound(audioClipName);
            StartCoroutine(StartSubtitle(audioClipName, audioManager.playerTalkingSource));
        }

        private IEnumerator StartSubtitle(string audioClipName, AudioSource audioSource)
        {
            var (sub, durations) = _subtitleManager.GetTextWithDurations(audioClipName);

            for (int i = 0; i < sub.Length; i++)
            {
                _textController.SetText(sub[i]);
                yield return new WaitForSeconds(durations[i]);
            }

            _textController.ClearText();
            audioSource.Stop();
        }
    }
}