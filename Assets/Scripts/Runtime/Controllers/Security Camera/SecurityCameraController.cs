using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Runtime.Controllers.Security_Camera
{
    public class SecurityCameraController : MonoBehaviour
    {
        public List<GameObject> cameras;
        private int cameraSelected; 

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                NextCam();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                PreviousCam();
            }
        }

        private void NextCam()
        {
            cameras[cameraSelected].SetActive(false); 

            cameraSelected = (cameraSelected + 1) % cameras.Count; 

            cameras[cameraSelected].SetActive(true); 
            Debug.Log("Selected Camera: " + cameraSelected);
        }

        private void PreviousCam()
        {
            cameras[cameraSelected].SetActive(false); 

            cameraSelected = (cameraSelected - 1 + cameras.Count) % cameras.Count;

            cameras[cameraSelected].SetActive(true); 
            Debug.Log("Selected Camera: " + cameraSelected);
        }
    }
}