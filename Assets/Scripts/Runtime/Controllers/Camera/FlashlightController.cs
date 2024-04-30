// using UnityEngine;
//
// public class FlashlightController : MonoBehaviour
// {
//     public Transform cameraTransform; // Kamera transformu
//     public Transform flashlightTransform; // Flashlight transformu
//     public float followSpeed = 5f; // Takip hızı
//     public float rotationSpeed = 10f; // Dönüş hızı
//
//     private Vector3 offset; // Kamera ile flashlight arasındaki offset
//
//     void Start()
//     {
//         offset = flashlightTransform.position - cameraTransform.position;
//     }
//
//     void Update()
//     {
//         // Kameranın konumunu ve rotasyonunu takip ederken biraz gecikmeli olarak güncelle
//         Vector3 targetPosition = cameraTransform.position + offset;
//         flashlightTransform.position = Vector3.Lerp(flashlightTransform.position, targetPosition, followSpeed * Time.deltaTime);
//
//         Quaternion targetRotation = Quaternion.LookRotation(cameraTransform.forward, cameraTransform.up);
//         flashlightTransform.rotation = Quaternion.Slerp(flashlightTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//     }
// }
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Transform cameraTransform; // Kamera transformu
    public Transform flashlightTransform; // Flashlight transformu
    public float followSpeed = 5f; // Takip hızı
    public float rotationSpeed = 10f; // Dönüş hızı
    public float maxRotationAngle = 30f; // Maksimum dönüş açısı

    private Vector3 offset; // Kamera ile flashlight arasındaki offset

    void Start()
    {
        offset = flashlightTransform.position - cameraTransform.position;
    }

    void Update()
    {
        // Kameranın konumunu ve rotasyonunu takip ederken biraz gecikmeli olarak güncelle
        Vector3 targetPosition = cameraTransform.position + offset;
        flashlightTransform.position = Vector3.Lerp(flashlightTransform.position, targetPosition, followSpeed * Time.deltaTime);

        // Mouse pozisyonunu al
        float mouseX = Input.GetAxis("Mouse X");

        // Hedef rotasyonu belirle
        Quaternion targetRotation = Quaternion.identity;
        if (mouseX != 0)
        {
            float angle = Mathf.Clamp(mouseX * maxRotationAngle, -maxRotationAngle, maxRotationAngle);
            targetRotation = Quaternion.Euler(0, angle, 0) * cameraTransform.rotation;
        }
        else
        {
            targetRotation = cameraTransform.rotation;
        }

        // Hedef rotasyona doğru flashlightin rotasyonunu lerp et
        flashlightTransform.rotation = Quaternion.Lerp(flashlightTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
