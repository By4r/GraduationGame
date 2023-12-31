using System.Collections;
using System.Collections.Generic;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Runtime.Enums;
using Runtime.Signals;


public class CapturePhotoController : MonoBehaviour
{
    [Header("Photo Taker")] 
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private bool isPhotoModeOpen;

    
    [Header("Flash Effect")] 
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Animations")] 
    [SerializeField] private Animator fadingAnimation;
    [SerializeField] private Animator removingAnimation;
    
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private PlayerAnomalyReportController _playerAnomalyReport;
    
    
    private Texture2D screenCapture;
    private bool viewingPhoto;

    [SerializeField] private int photoRemainCount;
   
    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,false);
        
        _playerManager = FindObjectOfType<PlayerManager>();
        _playerAnomalyReport = FindObjectOfType<PlayerAnomalyReportController>();
        //_playerManager = GameObject.Find("PhotoFlashLight").GetComponentInChildren<PlayerManager>();
        cameraFlash = _playerManager.light;
        
        
         //animatorr = FindObjectOfType<Animator>();

    }
   
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            OpenPhotoMode();
        }

        if (isPhotoModeOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!viewingPhoto&& !_playerAnomalyReport.anomalyOnReport)
                {
                    StartCoroutine(PhotoCapture());
                    
                }

            }
        }
       
    }
   
    private void OpenPhotoMode()
    {
        if (!isPhotoModeOpen)
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Photo, 2);
            isPhotoModeOpen = true;
        }
        else if(isPhotoModeOpen)
        {
            CoreUISignals.Instance.onClosePanel.Invoke(2);
            isPhotoModeOpen = false;
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
        
        screenCapture.ReadPixels(regionToRead,0,0,false);
        screenCapture.Apply();
        ShowPhoto();
        photoRemainCount--;
        Debug.Log("Photo Taken");
        StartCoroutine(PhotoRemoveEffect());
        StartCoroutine(_playerAnomalyReport.AnomalyReported());
        
    }
// EĞER 3 FOTO-- OLDUKTAN SONRA CANAVAR UYANMA SİNYALİ TETİKLENİR
  

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
}
