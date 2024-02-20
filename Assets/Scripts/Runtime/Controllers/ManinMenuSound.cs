using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManinMenuSound : MonoBehaviour
{
    [SerializeField] private AudioSource menuAudioSource;

    [SerializeField] private AudioClip menuAudioClip;
    // Start is called before the first frame update
    void Start()
    {
        menuAudioSource.PlayOneShot(menuAudioClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
