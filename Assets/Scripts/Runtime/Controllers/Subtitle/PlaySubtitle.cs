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
            var sub = _subtitleManager.GetText(audioSource.clip.name);
            var lineDuration = audioSource.clip.length / sub.Length;

            foreach (var line in sub)
            {
                _textController.SetText(line);
                yield return new WaitForSeconds(lineDuration);
            }
            
            Debug.Log(sub);
            _textController.ClearText();

        }
    }
}