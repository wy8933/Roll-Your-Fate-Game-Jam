using InventorySystem;
using UnityEngine;

public class CollectOxygen : MonoBehaviour, IInteractable
{
    public Transform Transform => gameObject.transform;

    [SerializeField]private string _prompt;

    public string Prompt => _prompt;

    public ItemSO OxygenItem;

    public bool CanInteract(GameObject player)
    {
        if (Inventory.Instance.IsAtCap()) 
        {
            return false;
        }
        return true;
    }

    public bool Interact()
    {
        Inventory.Instance.AddItem(OxygenItem);
        return true;
    }


}
