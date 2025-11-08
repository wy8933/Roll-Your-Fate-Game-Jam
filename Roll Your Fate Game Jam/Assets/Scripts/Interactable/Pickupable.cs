using UnityEngine;
using InventorySystem;

public class Pickupable : MonoBehaviour, IInteractable
{
    public Transform Transform => transform;

    [SerializeField] private string _prompt = "Interact";
    public string Prompt => _prompt;

    public ItemSO item;

    public bool CanInteract(GameObject player)
    {
        return true;
    }

    public bool Interact()
    {
        if (item == null) 
        {
            return false;
        }

        if (!Inventory.Instance.AddItem(item))
            return false;
        Debug.Log($"Picked: {item.itemID}");
        Inventory.Instance.LoadUI();
        Destroy(gameObject, 0.1f);
        return true;
    }
}