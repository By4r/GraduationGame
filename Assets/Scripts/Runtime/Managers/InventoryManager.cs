using System;
using System.Collections.Generic;
using Michsky.UI.Dark;
using Runtime.Data.UnityObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Managers
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;
        public List<CD_Item> Items = new List<CD_Item>();

        public Transform ItemContent;
        public GameObject InventoryItem;

        private void Awake()
        {
            Instance = this;
        }

        public void Add(CD_Item item)
        {
            Items.Add(item);
        }

        public void Remove(CD_Item item)
        {
            Items.Remove(item);
        }


        public void ListItems()
        {
            foreach (var item in Items)
            {
                GameObject obj = Instantiate(InventoryItem, ItemContent);
                
                var itemName = obj.transform.Find("Item/ItemName").GetComponent<TextMeshProUGUI>();
                var itemIcon = obj.transform.Find("Item/ItemIcon").GetComponent<Image>();
                
                itemName.text = item.ItemData.itemName;
                itemIcon.sprite = item.ItemData.icon;
            }
        }
        
    }
}