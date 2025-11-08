using InventorySystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteCollectionUIManager : MonoBehaviour
{
    public TMP_Text noteTitle;
    public TMP_Text noteText;

    public List<NoteItem> noteItems = new List<NoteItem>();
    CollectedStickyNoteSO collectedStickyNoteSO;

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
