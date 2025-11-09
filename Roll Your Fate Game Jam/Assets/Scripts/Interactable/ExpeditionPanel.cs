using UnityEngine;

namespace Interactable
{
    public class ExpeditionPanel : MonoBehaviour, IInteractable
    {
        public Transform Transform => transform;
        public string prompt;
        public string Prompt => prompt;
        public GameObject sectorSelectionUI;
        public Canvas targetCanvas;
            
        public bool CanInteract(GameObject player)
        {
            return true;
        }

        public bool Interact()
        {
            Instantiate(sectorSelectionUI, targetCanvas.transform);
            return true;
        }
    }
}
