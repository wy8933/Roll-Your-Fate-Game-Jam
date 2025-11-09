using InventorySystem;
using UnityEngine;

public class ReturnOxygen : MonoBehaviour, IInteractable
{
    public Transform Transform => transform;

    [SerializeField] private string _prompt;

    public string Prompt => _prompt;

    public ItemSO OxygenItem;

    public bool CanInteract(GameObject player)
    {
        return Inventory.Instance.ContainItem(OxygenItem.itemID);
    }

    public bool Interact()
    {
        if (!Inventory.Instance.ContainItem(OxygenItem.itemID)) 
        {
            return false;
        }

        Inventory.Instance.RemoveItem(OxygenItem.itemID);
        return true;
    }
}
