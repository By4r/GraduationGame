using System.Collections;
using System.Collections.Generic;
using Runtime.Controllers.Player;
using Runtime.Controllers.Security_Room;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;


public class CapturePhotoController : MonoBehaviour
{
    [Header("Photo Taker")] [SerializeField]
    private Image photoDisplayArea;

    [SerializeField] private GameObject photoFrame;
    [SerializeField] private bool isPhotoPanelOpen=false;

    [ShowInInspector] private bool isPauseState;


    [Header("Flash Effect")] [SerializeField]
    private GameObject cameraFlash;

    [SerializeField] private float flashTime;

    [Header("Photo Animations")] [SerializeField]
    private Animator fadingAnimation;

    [SerializeField] private Animator removingAnimation;

    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private PlayerAnomalyReportController _playerAnomalyReport;
    [SerializeField] private SecurityRoomController securityRoomController;
    [SerializeField] private PlayerPhysicsController _playerPhysicsController;
    private Texture2D screenCapture;
    private bool viewingPhoto;

    [SerializeField] public int photoRemainCount;


    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PauseSignals.Instance.onPhotoPanelState += OnPhotoPanelState;
        PauseSignals.Instance.onPauseState += OnPauseState;
        CaptureCameraSignals.Instance.onRemovePhoto += RemovePhoto;
        CaptureCameraSignals.Instance.onOpenPhotoMode += OpenPhotoMode;
        CaptureCameraSignals.Instance.onRemovePhoto += RemovePhoto;
        CaptureCameraSignals.Instance.onShowPhoto += ShowPhoto;
    }

    private void OnPauseState(bool state)
    {
        isPauseState = state;
    }


    private void UnSubscribeEvents()
    {
        PauseSignals.Instance.onPhotoPanelState -= OnPhotoPanelState;
        PauseSignals.Instance.onPauseState -= OnPauseState;
        CaptureCameraSignals.Instance.onOpenPhotoMode -= OpenPhotoMode;
        CaptureCameraSignals.Instance.onRemovePhoto -= RemovePhoto;
        CaptureCameraSignals.Instance.onShowPhoto -= ShowPhoto;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }


    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        _playerManager = FindObjectOfType<PlayerManager>();
        _playerAnomalyReport = FindObjectOfType<PlayerAnomalyReportController>();
        cameraFlash = _playerManager.light;
        securityRoomController = FindObjectOfType<SecurityRoomController>();
        _playerPhysicsController = FindObjectOfType<PlayerPhysicsController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !securityRoomController.isSecurityPanelOpen && !isPauseState)
        {
            OpenPhotoMode();
        }

        if (isPhotoPanelOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!viewingPhoto && !_playerAnomalyReport.anomalyOnReport)
                {
                    StartCoroutine(PhotoCapture());
                }
            }
        }
    }

    private void OpenPhotoMode()
    {
        if (!isPhotoPanelOpen)
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Photo, 2);
            isPhotoPanelOpen = true;
        }
        else if (isPhotoPanelOpen)
        {
            CoreUISignals.Instance.onClosePanel.Invoke(2);
            isPhotoPanelOpen = false;
            Debug.Log("panel closed");
        }
    }

    private void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
    }

    IEnumerator PhotoCapture()
    {
        viewingPhoto = true;
        yield return new WaitForEndOfFrame();

        _playerAnomalyReport.PlayerRaycast();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
        if (!_playerPhysicsController.isInsideSecRoom)
        {
            photoRemainCount--;
        }
        Debug.Log("Photo Taken");
        StartCoroutine(PhotoRemoveEffect());
        StartCoroutine(_playerAnomalyReport.AnomalyReported());
    }


    private void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture,
            new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height),
            new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);
        StartCoroutine(CameraFlashEffect());
        fadingAnimation.Play("PhotoFadeAnim");
    }

    IEnumerator PhotoRemoveEffect()
    {
        removingAnimation.Play("PhotoRemovingAnim");
        yield return new WaitForSeconds(1.5f);

        RemovePhoto();
    }

    IEnumerator CameraFlashEffect()
    {
        //audio play here
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }

    private void OnPhotoPanelState(bool state)
    {
        isPhotoPanelOpen = state;
    }
}