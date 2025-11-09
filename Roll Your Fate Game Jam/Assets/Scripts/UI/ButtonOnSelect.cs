using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonOnSelect : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        AudioManager.Instance.Select();
    }
}
