using System.Collections;
using System.Collections.Generic;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Runtime.Enums;
using Runtime.Signals;

public class CapturePhoto : MonoBehaviour
{
    [Header("Photo Taker")] 
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private bool isPhotoModeOpen;

    [FormerlySerializedAs("camereFlash")]
    [Header("Flash Effect")] 
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Animations")] 
    [SerializeField] private Animator fadingAnimation;
    [SerializeField] private Animator removingAnimation;
    
    [SerializeField] private PlayerManager _playerManager;
    
    private Texture2D screenCapture;
    private bool viewingPhoto;
    
    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,false);
        
        _playerManager = FindObjectOfType<PlayerManager>();
        //_playerManager = GameObject.Find("PhotoFlashLight").GetComponentInChildren<PlayerManager>();
        cameraFlash = _playerManager.light;
        
        
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
                if (!viewingPhoto)
                {
                    StartCoroutine(PhotoCapture());
                    StartCoroutine(PhotoRemoveEffect());
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

   // bool IsPanelOpen(){return;}
    private void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        
    }

    IEnumerator PhotoCapture()
    {
        viewingPhoto = true;
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        
        screenCapture.ReadPixels(regionToRead,0,0,false);
        screenCapture.Apply();
        ShowPhoto();
        Debug.Log("Photo Taken");
       
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
        yield return new WaitForSeconds(3);
        
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
