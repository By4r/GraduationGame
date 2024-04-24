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

        public TextMeshProUGUI subtitleText; // UI Text nesnesi
        public AudioClip speechAudioClip; // Konuşmanın ses dosyası
        public TextAsset speechTextFile; // Konuşmanın metni

        public Subtitle[] subtitles; // Alt yazı dizisi
        [SerializeField] private AudioSource audioSource; // Ses kaynağı

        private int currentSubtitleIndex = 1; // Şu anki alt yazı endeksi
        private bool isPlaying = false; // Ses dosyası oynuyor mu?

        void Start()
        {
            // Metin dosyasından alt yazıları oku
            if (speechTextFile != null)
            {
                // Alt yazıları parse et ve başlangıç ve bitiş zamanlarını belirle
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
            // Ses dosyası oynuyorsa ve alt yazılar varsa
            if (isPlaying && currentSubtitleIndex < subtitles.Length)
            {
                // UI'da alt yazıları güncelle
                if (audioSource.time >= subtitles[currentSubtitleIndex].startTime && audioSource.time <= subtitles[currentSubtitleIndex].endTime)
                {
                    subtitleText.text = subtitles[currentSubtitleIndex].text;
                }

                // Bir sonraki alt yazıya geç
                if (audioSource.time >= subtitles[currentSubtitleIndex].endTime)
                {
                    currentSubtitleIndex++;
                    
                    // Tüm alt yazılar tamamlandıysa metni kapat
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
            // Ses dosyasını başlat
            audioSource.Play();

            isPlaying = true;
        }
    }
}
