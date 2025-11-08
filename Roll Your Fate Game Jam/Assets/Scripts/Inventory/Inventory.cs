using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<ItemSO> items = new List<ItemSO>();
    public List<ItemBlockUI> itemBlocks = new List<ItemBlockUI>();

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        LoadUI();
    }

    public void AddItem(ItemSO item) 
    {
        if(items.Count <= 5 && item !=null)
            items.Add(item);
    }

    public void RemoveItem(string itemID) 
    {
        if(items.Count ==0)
            return;

        foreach (ItemSO item in items) 
        {
            if (item.itemID == itemID) 
            {
                items.Remove(item);
                return;
            }
        }
    }

    public void LoadUI() 
    {
        for (int i = 0; i < items.Count; i++) 
        {
            itemBlocks[i].SetIcon(items[i].itemIcon);
        }
    }
}
