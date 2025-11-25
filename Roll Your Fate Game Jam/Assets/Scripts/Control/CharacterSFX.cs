using Control;
using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    [SerializeField] private AudioClip walkSFX;

    public void PlayWalkSFX()
    {
        PlayerController playerController = GetComponentInParent<PlayerController>();
        if(playerController.isOnGround)
            AudioManager.Instance.PlaySFX(walkSFX);
    }
}
