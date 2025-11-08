using InventorySystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteCollectionUIManager : MonoBehaviour
{
    public TMP_Text noteTitle;
    public TMP_Text noteText;

    public List<NoteItem> noteItems = new List<NoteItem>();

    public void SetUp(CollectedStickyNoteSO collectedStickyNoteSO) 
    {
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
}
