using Cinemachine;
using Runtime.Controllers.Camera;
using UnityEngine;
using UnityEngine.Playables;

namespace Runtime.Controllers.Item_Inventory
{
    public class CarPathController : MonoBehaviour
    {
        
        public CharacterController playerController;
        public PlayableDirector timelineDirector;
        public CameraController cameraController;
        public GameObject cinemachinePath;
        [SerializeField] private CinemachineDollyCart _cinemachineDollyCart;
        void Start()
        {
            // Timeline başlamadan önce karakter ve kamera kontrolünü devre dışı bırak
            // Timeline'ı başlat
            timelineDirector.Play();
        }

        void Update()
        {
            // Timeline bittiğinde karakter ve kamera kontrolünü yeniden etkinleştir
            if (timelineDirector.state != PlayState.Playing)
            {
                playerController.enabled = true;
                cameraController.mouseState = true;
                _cinemachineDollyCart.enabled = false;
            }
            else
            {
                playerController.enabled = false;
                cameraController.mouseState = false;

                
            }
        }
    }
}