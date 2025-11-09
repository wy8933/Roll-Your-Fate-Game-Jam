using Control;
using InventorySystem;
using UnityEngine;

public class NoteCollection : MonoBehaviour, IInteractable
{
    public static NoteCollection Instance;

    public Transform Transform => gameObject.transform;

    [SerializeField]private string _prompt;
    public string Prompt => _prompt;

    public CollectedStickyNoteSO collectedStickyNoteSO;

    public GameObject collectedStickyNotePanel;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            collectedStickyNoteSO.collectedNote.Clear();
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public bool CanInteract(GameObject player)
    {
        return true;
    }

    public bool Interact()
    {
        collectedStickyNotePanel.SetActive(true);
        collectedStickyNotePanel.GetComponent<NoteCollectionUIManager>().SetUp(collectedStickyNoteSO);
        PlayerInputHandler.Instance.SwitchTo(ActionMap.UI);
        PlayerInputHandler.Instance.RightClick += CancelInteraction;
        return true;
    }

    public void CancelInteraction() 
    {
        collectedStickyNotePanel.SetActive(false);
        PlayerInputHandler.Instance.SwitchTo(ActionMap.Player);
        PlayerInputHandler.Instance.RightClick -= CancelInteraction;
    }
}
