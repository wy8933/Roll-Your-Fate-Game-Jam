using Control;
using InventorySystem;
using UI.Minimap;
using UnityEngine;

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
        gameObject.SetActive(false);
        PlayerInteraction.Instance.RemoveInteractable(this);
        MinimapManager.Instance.Remove(gameObject);
        return true;
    }
}