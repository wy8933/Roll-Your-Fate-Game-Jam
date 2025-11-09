using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    [SerializeField] private AudioClip walkSFX;

    public void PlayWalkSFX()
    {
        AudioManager.Instance.PlaySFX(walkSFX);
    }
}
