using TMPro;
using UnityEngine;

public class NoteItem : MonoBehaviour
{
    public TMP_Text noteID;
    public TMP_Text noteTitle;

    public void SetNote(int id, string name) 
    {
        noteID.text = "LOG " + id.ToString();
        noteTitle.text = name;   
    }
}
