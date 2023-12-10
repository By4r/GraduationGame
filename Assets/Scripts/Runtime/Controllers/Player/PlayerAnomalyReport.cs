using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerAnomalyReport : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        public GameObject playerLook;
        // Update is called once per frame
        void Update()
        {
            if (Physics.Raycast(playerLook.transform.position, playerLook.transform.TransformDirection(Vector3.forward),
                    out RaycastHit hitInfo, 20f))
            {
                Debug.Log("hit");
                Debug.DrawRay(playerLook.transform.position, playerLook.transform.TransformDirection(Vector3.forward) * hitInfo.distance,
                    Color.green);

            }
            else
            {
                Debug.Log("no hit");
                Debug.DrawRay(playerLook.transform.position, playerLook.transform.TransformDirection(Vector3.forward) * 20f,
                    Color.red);
            }
        }

    }
    }
