using System.Collections.Generic;
using UnityEngine;
using UI;

namespace InventorySystem
{ 
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        public List<ItemSO> items = new List<ItemSO>();
        public List<ItemBlockUI> itemBlocks = new List<ItemBlockUI>();

        public int maxItemCount = 5;
        private void Awake()
        {
            if(Instance == null) 
            { 
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void Start()
        {
            LoadUI();
        }

        public bool AddItem(ItemSO item) 
        {
            // if the max item cound is not met, add it to the inventory
            if (items.Count < maxItemCount && item != null)
            {
                items.Add(item);
                LoadUI();
                return true;
            }
            else
                return false;
        }

        public void RemoveItem(string itemID) 
        {
            if(items.Count ==0)
                return;

            // Iterate to find the item with the same id and remove it
            foreach (ItemSO item in items) 
            {
                if (item.itemID == itemID) 
                {
                    items.Remove(item);
                    LoadUI();
                    return;
                }
            }
        }

        /// <summary>
        /// Update the Inventory UI with the icons
        /// </summary>
        public void LoadUI() 
        {
            int current = items.Count;

            for (int i = 0; i < items.Count; i++) 
            {
                itemBlocks[i].SetIcon(items[i].itemIcon);
            }

            for(int i = current; i < maxItemCount; i++) 
            {
                itemBlocks[i].SetIconAlphaZero();
            }
        }

        public bool IsAtCap() 
        {
            if (items.Count >= maxItemCount) 
            {
                return true;
            }

            return false;
        }

        public bool ContainItem(string itemID) 
        {
            foreach (ItemSO i in items) 
            {
                if (i.itemID == itemID) 
                {
                    return true;
                }
            }

            return false;
        }

        public int ItemCount(ItemSO item) 
        {
            int count = 0;

            foreach (ItemSO i in items) 
            {
                if (i.itemID == item.itemID) 
                {
                    count++;
                }
            }

            return count;
        }
    }
}