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
        Inventory.Instance.AddItem(item);
        Destroy(gameObject);
        Inventory.Instance.LoadUI();
        return true;
    }
}
