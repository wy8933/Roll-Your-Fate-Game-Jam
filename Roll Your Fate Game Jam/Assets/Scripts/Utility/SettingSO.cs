using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingSO", menuName = "SettingSO")]
public class SettingSO : ScriptableObject
{
    public UtilitySetting UtilitySetting;
    public GameSetting GameSetting;
}

[Serializable]
public class UtilitySetting
{
    public float mouseSensitivity;
    public float audioVolume;
}

[Serializable]
public class GameSetting
{
    public float initialOxygen;
    public float oxygenConsumingRate;
}
