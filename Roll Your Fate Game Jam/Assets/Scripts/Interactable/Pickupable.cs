using UnityEngine;

public class Pickupable : MonoBehaviour, IInteractable
{
    public Transform Transform => gameObject.transform;

    [SerializeField] private string _prompt = "Interact";
    public string Prompt => _prompt;

    public bool CanInteract(GameObject player)
    {
        return true;
    }

    public bool Interact()
    {


        return false;
    }
}
