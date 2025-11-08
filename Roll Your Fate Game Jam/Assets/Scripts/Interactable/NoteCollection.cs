using InventorySystem;
using UnityEngine;

public class NoteCollection : MonoBehaviour, IInteractable
{
    public static NoteCollection Instance;

    public Transform Transform => gameObject.transform;

    private string _prompt;
    public string Prompt => _prompt;

    public CollectedStickyNoteSO collectedStickyNoteSO;

    public GameObject collectedStickyNotePanel;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public bool CanInteract(GameObject player)
    {
        return false;
    }

    public bool Interact()
    {
        collectedStickyNotePanel.SetActive(true);
        return false;
    }
}
