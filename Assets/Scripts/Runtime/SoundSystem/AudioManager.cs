using System;
using UnityEngine;

namespace Runtime.SoundSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        [Space] public Sound[] environmentSounds, stateSounds, interactSounds, playerTalkingSounds;

        [Space(10)] public AudioSource environmentSource, stateSource, interactSource, playerTalkingSource;

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

        public void PlayEnvironmentSound(string name)
        {
            Sound s = Array.Find(environmentSounds, x => x.name == name);
            if (s == null)
            {
                Debug.Log("Sound Not Found");
                return;
            }
            environmentSource.clip = s.clip;
            environmentSource.loop = true;
            environmentSource.Play();
        }

        public void PlayStateSounds(string name)
        {
            Sound s = Array.Find(stateSounds, x => x.name == name);
            if (s == null)
            {
                Debug.Log("Sound Not Found");
                return;
            }
            stateSource.clip = s.clip;
            stateSource.loop = false; // Sadece bir kere çalmak için
            stateSource.Play();
        }

        public void StopStateSound()
        {
            if (stateSource.isPlaying)
            {
                stateSource.Stop();
            }
        }

        public void PlayInteractSounds(string name)
        {
            Sound s = Array.Find(interactSounds, x => x.name == name);
            if (s == null)
            {
                Debug.Log("Sound Not Found");
                return;
            }
            interactSource.PlayOneShot(s.clip);
        }

        public void PlayPlayerTalkingSound(string name)
        {
            Sound s = Array.Find(playerTalkingSounds, x => x.name == name);
            if (s == null)
            {
                Debug.Log("Sound Not Found");
                return;
            }
            playerTalkingSource.clip = s.clip;
            playerTalkingSource.loop = false; // Sadece bir kere çalmak için
            playerTalkingSource.Play();
        }

        public void StopPlayerTalkingSound()
        {
            if (playerTalkingSource.isPlaying)
            {
                playerTalkingSource.Stop();
            }
        }
    }
}
