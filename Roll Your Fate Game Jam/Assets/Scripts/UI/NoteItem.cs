using TMPro;
using UnityEngine;

public class NoteItem : MonoBehaviour
{
    public TMP_Text noteID;
    public TMP_Text noteText;

    public void SetNote(int id, string text) 
    {
        noteID.text = "LOG " + id.ToString();
        noteText.text = text;   
    }
}
