using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Controllers
{
    public class PlaySubtitle:MonoBehaviour
    {
        private AudioSource audioSource;
        [SerializeField] private SubtitleManager _subtitleManager;
        [SerializeField] private TextController _textController;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            _textController.ClearText();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
               
                
                audioSource.Play();
                Debug.Log("Text Key: " + audioSource.clip.name);

                StartCoroutine(StartSubtitle());
            }
        }

        private IEnumerator StartSubtitle()
        {
            var (sub, durations)  = _subtitleManager.GetTextWithDurations(audioSource.clip.name);
            

            for (int i = 0; i < sub.Length; i++)
            {
                _textController.SetText(sub[i]);
                yield return new WaitForSeconds(durations[i]);
            }

            _textController.ClearText();

        }
    }
}