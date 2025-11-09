using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteItem : MonoBehaviour
{
    public TMP_Text noteID;
    public TMP_Text noteTitle;

    [SerializeField] private float _selectedScale = 1.5f;

    public void SetNote(int id, string name) 
    {
        noteID.text = "LOG " + id.ToString();
        noteTitle.text = name;   
    }

    public void SetSelected(bool selected)
    {
        if (noteTitle != null)
            noteTitle.rectTransform.localScale = selected ? Vector3.one * _selectedScale : Vector3.one;
    }
}
