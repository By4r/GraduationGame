using System.Collections;
using System.Collections.Generic;
using Runtime.Controllers.Camera;
using Runtime.Controllers.Player;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

namespace Runtime.Controllers.Item
{
    public class ExamineObjectsController : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] private PlayerMovementController _playerMovementController;
        [SerializeField] private GameObject offset;
        [SerializeField] private bool isExamining;
        
        private Vector3 lastMousePosition;
        private Transform examinedObject; // Store the currently examined object
        private Animator animator;
        
    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();
    

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {

            Ray raycast = playerPhysicsController.GetRaycast();
            float range = playerPhysicsController.range;
            RaycastHit hit;

            if (Physics.Raycast(raycast, out hit, range))
                {
                    if (hit.collider.CompareTag("ExamineObject"))
                    {
                        ToggleExamination();
                        // Store the currently examined object and its original position and rotation
                        if (isExamining)
                        {
                        
                            examinedObject = hit.transform;
                            originalPositions[examinedObject] = examinedObject.position;
                            originalRotations[examinedObject] = examinedObject.rotation;
                        }
                        else
                        {
                            StopExamination();
                        }
                    }
                }
        }

        if (!isExamining)
        {
            NonExamine();
            
        }
        else
        {
            Examine();
            StartExamination();
        }
    }

    private void ToggleExamination()
    {
        isExamining = !isExamining;

    }
    
    void StartExamination()
    {
        lastMousePosition = Input.mousePosition;
        _cameraController.mouseState = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _playerMovementController.canMove = false;
        Debug.Log("ee panel opened");
    }
    
    void StopExamination()
    {
        _cameraController.mouseState = true;
        Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
       _playerMovementController.canMove = true;
       Debug.Log("ee panel close");
    }
    
    void Examine()
    {
        if (examinedObject != null)
        {
            examinedObject.position = Vector3.Lerp(examinedObject.position, offset.transform.position, 0.2f);

            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            float rotationSpeed = 1.0f;
            examinedObject.Rotate(deltaMouse.x * rotationSpeed * Vector3.up, Space.World);
            examinedObject.Rotate(deltaMouse.y * rotationSpeed * Vector3.left, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }
    void NonExamine()
    {
        if (examinedObject != null)
        {
           
            if (originalPositions.ContainsKey(examinedObject))
            {
                examinedObject.position = Vector3.Lerp(examinedObject.position, originalPositions[examinedObject], 0.2f);
            }
            if (originalRotations.ContainsKey(examinedObject))
            {
                examinedObject.rotation = Quaternion.Slerp(examinedObject.rotation, originalRotations[examinedObject], 0.2f);
            }
        }
    }

    }
}
