using Game;
using UnityEngine;

namespace Interactable
{
    public class SendBackHome : MonoBehaviour, IInteractable
    {
        public string _prompt;
        public string Prompt { get; }
        
        public Transform Transform => transform;

        public bool CanInteract(GameObject player)
        {
            return true;
        }

        public bool Interact()
        {
            GameManager.Instance.SendBackHome();
            return true;
        }
    }
}