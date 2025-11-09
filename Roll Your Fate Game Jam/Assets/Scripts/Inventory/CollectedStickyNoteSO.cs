using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem { 
    [CreateAssetMenu(fileName = "CollectedStickyNoteSO", menuName = "CollectedStickyNoteSO")]
    public class CollectedStickyNoteSO : ScriptableObject
    {
        public List<NoteData> collectedNote = new List<NoteData>();

        public void AddCollectedNote(NoteData data) 
        {
            collectedNote.Add(data);
        }
    }
}