using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Sprite itemIcon;
}
