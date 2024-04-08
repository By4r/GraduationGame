using System;
using Runtime.Data.UnityObjects;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers.Item
{
    public class ItemPickUpController:  MonoBehaviour
    {
        public CD_Item item;

        internal void Pickup()
        {
            InventoryManager.Instance.Add(item);
            Destroy(gameObject);
        }
    }
}