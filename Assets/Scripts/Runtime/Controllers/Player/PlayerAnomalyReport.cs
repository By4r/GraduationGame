using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerAnomalyReport : MonoBehaviour
    {
        [SerializeField] LayerMask layerMask;
        [SerializeField] public bool isAnomalyDetected;
        public GameObject playerLook;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        
        // Update is called once per frame
        void Update()
        {
            //PlayerRaycast();
        }
        
        public void PlayerRaycast()
        {
            if (Physics.Raycast(playerLook.transform.position, playerLook.transform.TransformDirection(Vector3.forward),
                    out RaycastHit hitInfo, 20f,layerMask,QueryTriggerInteraction.Collide))
            {
                Debug.Log("hit");
                Debug.DrawRay(playerLook.transform.position, playerLook.transform.TransformDirection(Vector3.forward) * hitInfo.distance,
                    Color.green);
                isAnomalyDetected = true;

            }
            else
            {
                Debug.Log("no hit");
                Debug.DrawRay(playerLook.transform.position, playerLook.transform.TransformDirection(Vector3.forward) * 20f,
                    Color.red);
                isAnomalyDetected = false;
            }

            if (isAnomalyDetected)
            {
                StartCoroutine(AnomalyDetected());
            }
            else StartCoroutine(AnomalyNotDetected());
        }
        
        IEnumerator AnomalyDetected()
        {
            //removingAnimation.Play("PhotoRemovingAnim");
            Debug.Log("anomalydeteceted");
            yield return new WaitForSeconds(1.5f);
        
            
        }
        IEnumerator AnomalyNotDetected()
        {
            Debug.Log("anomalydetecetedNO");
            //removingAnimation.Play("PhotoRemovingAnim");
            yield return new WaitForSeconds(1.5f);
        
            
        }
        
    }
    }
