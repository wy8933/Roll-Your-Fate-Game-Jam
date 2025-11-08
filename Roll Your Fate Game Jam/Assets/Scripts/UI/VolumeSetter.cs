using UnityEngine;
using UnityEngine.Audio;

namespace UI
{
    public class VolumeSetter : MonoBehaviour
    {
        [SerializeField]AudioMixerGroup audioMixerGroup;
        bool isMuted;
        float volume = 1;
        public void SetVolume(float value)
        {
            volume = value;
            if (isMuted)
                return;
            UpdateVolume(value);
        }
    
        public void SetMute(bool isMuted)
        {
            this.isMuted = isMuted;
            if (isMuted)
                UpdateVolume(0f);
            else
                UpdateVolume(volume);
        }

        void UpdateVolume(float value)
        {
            float dB = value < 0.001f? -80f : Mathf.Log10(value) * 20f;
            string paramName;
            switch (audioMixerGroup.name)
            {
                case "Master":
                    paramName = "MasterVolume";
                    break;
                case "Music":
                    paramName = "MusicVolume";
                    break;
                case "SFX":
                    paramName = "SFXVolume";
                    break;
                default:
                    Debug.LogError($"Unknown Audio Group Name: {audioMixerGroup.name}");
                    paramName = "";
                    break;
            }
            audioMixerGroup.audioMixer.SetFloat(paramName, dB);
        }
    }

}