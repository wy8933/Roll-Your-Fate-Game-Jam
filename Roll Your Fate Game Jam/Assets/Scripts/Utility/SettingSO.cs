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
    // public float masterVolume;
    // public float musicVolume;
    // public float sfxVolume;
}

[Serializable]
public class GameSetting
{
    public float initialOxygen;
    public float oxygenConsumingRate;
}
