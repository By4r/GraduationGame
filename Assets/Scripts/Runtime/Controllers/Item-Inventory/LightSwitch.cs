using System.Collections.Generic;
using Runtime.SoundSystem;
using UnityEngine;

namespace Runtime.Controllers.Item_Inventory
{
    public class LightSwitch : MonoBehaviour
    {
        public GameObject controlledObject;
        [SerializeField] private AudioManager _audioManager;
        

        public void ToggleObject()
        {
            _audioManager.PlayInteractSounds("LightSwitchSound");
            controlledObject.SetActive(!controlledObject.activeSelf);
        }
    }

}