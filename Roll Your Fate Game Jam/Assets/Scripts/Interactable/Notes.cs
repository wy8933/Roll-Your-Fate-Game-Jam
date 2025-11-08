using UnityEngine;
using InventorySystem;
using UI;
using Control;

[System.Serializable]
public struct NoteData 
{
    public int ID;
    public string Name; 
    public string Description;
}

public class Notes : MonoBehaviour, IInteractable
{
    public Transform Transform => transform;

    [SerializeField] private string _prompt = "Hello";
    public string Prompt => _prompt;

    public NoteData noteData;

    public bool CanInteract(GameObject player)
    {
        return true;
    }

    public bool Interact()
    {
        NoteCollection.Instance.collectedStickyNoteSO.AddCollectedNote(noteData);
        gameObject.SetActive(false);
        PlayerInteraction.Instance.RemoveInteractable(this);
        return true;
    }
}
