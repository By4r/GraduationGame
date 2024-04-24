using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Runtime.Controllers
{
    public class SubtitleManager : MonoBehaviour
    {
        [System.Serializable]
        public struct Subtitle
        {
            public string text;
            public float startTime;
            public float endTime;
        }

        public TextMeshProUGUI subtitleText;
        public AudioClip speechAudioClip;
        public TextAsset speechTextFile;

        public Subtitle[] subtitles;
        [SerializeField] private AudioSource audioSource;

        private int currentSubtitleIndex = 1;
        private bool isPlaying = false;

        void Start()
        {
            
            if (speechTextFile != null)
            {
                
                string[] lines = speechTextFile.text.Split('\n');
                //subtitles = new Subtitle[lines.Length];
                //float clipLength = speechAudioClip.length;
                for (int i = 0; i < lines.Length; i++)
                {
                    subtitles[i].text = lines[i];
                    subtitles[i].startTime = (i / (float)lines.Length);
                    subtitles[i].endTime = ((i + 1) / (float)lines.Length);
                    //subtitles[i].startTime = (i / (float)lines.Length) * clipLength;
                    //subtitles[i].endTime = ((i + 1) / (float)lines.Length) * clipLength;
                }
            }

            //audioSource = GetComponent<AudioSource>();
            audioSource.clip = speechAudioClip;
        }

        void Update()
        {
            
            if (isPlaying && currentSubtitleIndex < subtitles.Length)
            {
                
                if (audioSource.time >= subtitles[currentSubtitleIndex].startTime && audioSource.time <= subtitles[currentSubtitleIndex].endTime)
                {
                    subtitleText.text = subtitles[currentSubtitleIndex].text;
                }

                
                if (audioSource.time >= subtitles[currentSubtitleIndex].endTime)
                {
                    currentSubtitleIndex++;
                    
                    
                    if (currentSubtitleIndex >= subtitles.Length)
                    {
                        subtitleText.text = "";
                    }
                }
            }
        }

        [Button("START SPEECH BUTTON")]
        public void StartSpeech()
        {
            audioSource.Play();

            isPlaying = true;
        }
    }
}
