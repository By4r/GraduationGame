using System.Collections;
using System.Collections.Generic;
using Runtime.Controllers.Player;
using Runtime.Controllers.Task_Tab;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Runtime.Controllers.Camera
{
    public class OutlineController : MonoBehaviour
{
    [ShowInInspector] private Transform highlight;
    //private RaycastHit raycastHit;
    
    [SerializeField] private PlayerPhysicsController playerPhysicsController;

    
    
    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        Ray raycast = playerPhysicsController.GetRaycast();
        float range = playerPhysicsController.range;
        
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * range), Color.green);
        if (Physics.Raycast(raycast, out RaycastHit hit, range)) 
        {
            highlight = hit.transform;
            if (highlight.CompareTag("Selectable") )
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            /*if (highlight.CompareTag("Collectable") )
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.cyan;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }*/
            else
            {
                highlight = null;
            }
        }
        
    }

}
}