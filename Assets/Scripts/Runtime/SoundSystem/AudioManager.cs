using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.SoundSystem
{
    public class AudioManager:MonoBehaviour
    {

        public static AudioManager Instance;
        
        [Space] public Sound[] environmentSounds, stateSounds, interactSounds;

        [Space(10)]
        public AudioSource environmentSource, stateSource, interactSource;


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
            }
            else
            {
                environmentSource.clip = s.clip;
                environmentSource.loop = true;
                environmentSource.Play();
            }
            

        }

        public void PlayStateSounds(string name)
        {
            Sound s = Array.Find(stateSounds, x => x.name == name);

            if (s == null)
            {
                Debug.Log("Sound Not Found");
            }
            
            if (!stateSource.isPlaying)
            {
                stateSource.clip = s.clip;
                stateSource.loop = true;
                stateSource.Play();
            }
            
            // else
            // {
            //     stateSource.PlayOneShot(s.clip);
            // }
        }

        public void StopStateSound()
        {
            if (stateSource.isPlaying)
            {
                stateSource.Stop();
                stateSource.loop = false;
            }
        }
        
        
        public void PlayInteractSounds(string name)
        {
            Sound s = Array.Find(interactSounds, x => x.name == name);

            if (s == null)
            {
                Debug.Log("Sound Not Found");
            }
            else
            {
                interactSource.PlayOneShot(s.clip);
            }
        }
        
    }
}