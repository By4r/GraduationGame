using System;
using UnityEngine;

namespace Runtime.Controllers
{
    public class FirePlaceController:MonoBehaviour
    {
        private bool inWoodArea = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wood"))
            {
                inWoodArea = true;
            }
        }

        public bool IsInWoodArea()
        {
            return inWoodArea;
        }
    }
}