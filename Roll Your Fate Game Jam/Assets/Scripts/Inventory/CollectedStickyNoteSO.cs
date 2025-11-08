using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem { 
    [CreateAssetMenu(fileName = "CollectedStickyNoteSO", menuName = "CollectedStickyNoteSO")]
    public class CollectedStickyNoteSO : ScriptableObject
    {
        public Dictionary<int, string> collectedNote = new Dictionary<int, string>();

        public void AddCollectedNote(int id, string text) 
        {
            collectedNote.Add(id, text);
        }
    }
}