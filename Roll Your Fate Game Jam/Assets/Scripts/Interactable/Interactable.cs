using UnityEngine;

public interface IInteractable
{
    Transform Transform { get; }
    string Prompt { get; }
    public bool CanInteract(GameObject player);
    public bool Interact();
}
