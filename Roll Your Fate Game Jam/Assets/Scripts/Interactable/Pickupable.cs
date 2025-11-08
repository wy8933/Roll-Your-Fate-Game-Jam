using UnityEngine;

public class Pickupable : MonoBehaviour, IInteractable
{
    public Transform Transform => gameObject.transform;

    [SerializeField] private string _prompt = "Interact";
    public string Prompt => _prompt;

    public ItemSO item;

    public bool CanInteract(GameObject player)
    {
        return true;
    }

    public bool Interact()
    {
        Inventory.Instance.AddItem(item);

        return false;
    }
}
