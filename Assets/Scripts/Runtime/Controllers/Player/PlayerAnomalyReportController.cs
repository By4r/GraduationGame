using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Runtime.Managers;
using Runtime.Signals;

namespace Runtime.Controllers.Player
{
    public class PlayerAnomalyReportController : MonoBehaviour
    {
        [SerializeField] LayerMask layerMask;
        [SerializeField] public bool isAnomalyDetected;
        [SerializeField] private TextMeshProUGUI anomalyDetectionText;
        [SerializeField] private Animator AnomalyDetectionAnim;
        [SerializeField] private GameObject playerlook;
        //[SerializeField] private CapturePhoto _capturePhoto;
        [SerializeField] private PlayerManager _playerManager;

        public bool anomalyOnReport;
        // Start is called before the first frame update
        void Start()
        {
            // _capturePhoto.animatorr = FindObjectOfType<Animator>();
            // AnomalyDetectionAnim = _capturePhoto.animatorr;
            _playerManager = FindObjectOfType<PlayerManager>();
            playerlook = _playerManager.playerEyes;
        }

        
        // Update is called once per frame
        void Update()
        {
            //PlayerRaycast();
        }
        
        public void PlayerRaycast()
        {
            if (Physics.Raycast(playerlook.transform.position, playerlook.transform.TransformDirection(Vector3.forward),
                    out RaycastHit hitInfo, 20f,layerMask,QueryTriggerInteraction.Collide))
            {
                Debug.Log("hit");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitInfo.distance,
                    Color.green);
                isAnomalyDetected = true;

            }
            else
            {
                Debug.Log("no hit");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f,
                    Color.red);
                isAnomalyDetected = false;
            }
            //StartCoroutine(AnomalyReported());
        }
        
        public IEnumerator AnomalyReported()
        {
            anomalyOnReport = true;
            AnomalyDetectionAnim.enabled = true;
            AnomalyDetectionAnim.Play("AnomalyReported", -1, 0f);
            anomalyDetectionText.text = "Checking Anomaly...";
            yield return new WaitForSeconds(5f);

            if (isAnomalyDetected)
            {
                anomalyDetectionText.text = "Anomaly Fixed.";
                AnomalySignals.Instance.onAnomalyReport?.Invoke();
            }
            else
                anomalyDetectionText.text = "Anomaly Not Found.";
            yield return new WaitForSeconds(3f);
            AnomalyDetectionAnim.enabled = false;
            anomalyOnReport = false;

        }
        
        
    }
    }
