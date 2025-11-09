using InventorySystem;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NoteCollectionUIManager : MonoBehaviour
{
    public TMP_Text noteTitle;
    public TMP_Text noteText;

    public List<NoteItem> noteItems = new List<NoteItem>();
    CollectedStickyNoteSO collectedStickyNoteSO;

    [SerializeField] private InputActionReference _navigateAction;
    [SerializeField] private InputActionReference _cancelAction;

    public int currentIndex;
    public ScrollRect scrollRect;

    private void OnEnable()
    {
        _navigateAction?.action.Enable();
        _cancelAction?.action.Enable();

        if (_navigateAction != null) { _navigateAction.action.performed += OnNavigate; _navigateAction.action.canceled += OnNavigate; }
        if (_cancelAction != null) _cancelAction.action.performed += OnCancel;
    }

    private void OnDisable()
    {
        if (_navigateAction != null) { _navigateAction.action.performed -= OnNavigate; _navigateAction.action.canceled -= OnNavigate; _navigateAction.action.Disable(); }
        if (_cancelAction != null) { _cancelAction.action.performed -= OnCancel; _cancelAction.action.Disable(); }
    }

    private void OnCancel(InputAction.CallbackContext _) => gameObject.SetActive(false);

    private void OnNavigate(InputAction.CallbackContext context)
    {
        var axis = context.ReadValue<Vector2>();
        if (!context.performed) return;

        const float deadzone = 0.3f;
        int stepX = axis.x < -deadzone ? -1 : (axis.x > deadzone ? 1 : 0);
        int stepY = axis.y < -deadzone ? -1 : (axis.y > deadzone ? 1 : 0);

        int last = Mathf.Max(0, noteItems.Count - 1);
        currentIndex = MoveCursor(currentIndex, stepX, stepY, 1, last);

        if (currentIndex > 7)
        {
            scrollRect.verticalScrollbar.value = 0;
        }
        else 
        {
            scrollRect.verticalScrollbar.value = 1;
        }

        ShowFullNote(currentIndex);

        UpdateHighlights();
    }

    private static int MoveCursor(int index, int stepX, int stepY, int columns, int maxIndex)
    {
        int row = index / columns;
        int col = index % columns;

        col += stepX;
        row -= stepY;

        col = Mathf.Clamp(col, 0, columns - 1);

        int target = row * columns + col;
        return Mathf.Clamp(target, 0, Mathf.Max(0, maxIndex));
    }

    private void UpdateHighlights()
    {
        int maxIndex = Mathf.Max(0, noteItems.Count - 1);

        maxIndex = Mathf.Clamp(currentIndex, 0, maxIndex);

        SetSelectionVisuals(noteItems, currentIndex);
    }

    private static void SetSelectionVisuals(List<NoteItem> slots, int selectedIndex)
    {
        if (slots == null) return;
        for (int i = 0; i < slots.Count; i++)
            slots[i].SetSelected(i == selectedIndex);
    }

    public void SetUp(CollectedStickyNoteSO collectedStickyNoteSO) 
    {
        this.collectedStickyNoteSO = collectedStickyNoteSO;
        for (int i = 0; i < 15; i++) 
        {
            bool founded = false;
            foreach (NoteData noteData in collectedStickyNoteSO.collectedNote) 
            {
                if (noteData.ID == i) 
                {
                    noteItems[i].SetNote(noteData.ID, noteData.Name);
                    founded = true;
                }
            }

            if (!founded) 
            {
                noteItems[i].SetNote(i, "Not Collected");
            }
        }
    }

    public void ShowFullNote(int id) 
    {
        NoteData currentData = new NoteData();
        bool founded = false;

        foreach (NoteData data in collectedStickyNoteSO.collectedNote) 
        {
            if (data.ID == id) 
            {
                currentData = data;
                founded = true;
                break;
            }
        }

        if (founded)
        {
            noteTitle.text = currentData.Name;
            noteText.text = currentData.Text;
        }
        else 
        {
            noteTitle.text = id + " Not Collected";
            noteText.text = "Note not collected";
        }
    }
}
