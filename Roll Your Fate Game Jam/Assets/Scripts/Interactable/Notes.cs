using UnityEngine;
using InventorySystem;
using UI;

public class Notes : MonoBehaviour, IInteractable
{
    public Transform Transform => transform;

    [SerializeField] private string _prompt = "Hello";
    public string Prompt => _prompt;

    public bool CanInteract(GameObject player)
    {
        return true;
    }

    public bool Interact()
    {
        HUDManager.Instance.SetText(_prompt);
        return true;
    }
}
