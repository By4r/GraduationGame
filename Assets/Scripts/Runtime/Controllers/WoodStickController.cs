using DG.Tweening;
using UnityEngine;

namespace Runtime.Controllers
{
    public class WoodStickController:MonoBehaviour
    {

        [SerializeField] private GameObject woodObject;
        [SerializeField] private Animator animatorWood;

        internal void ActiveWoodObject()
        {
            woodObject.SetActive(true);
            Debug.Log("WOOD OBJECT ACTIVE!");
        }

        internal void DeActiveWoodObject()
        {
            woodObject.SetActive(false);
        }
        
        internal void HitWood()
        {
            DOVirtual.DelayedCall(5f, () => animatorWood.Play("woodenstick"));
        }

    }
}