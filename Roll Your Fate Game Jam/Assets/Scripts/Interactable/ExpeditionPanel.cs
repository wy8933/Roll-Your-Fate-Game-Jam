using System.Collections.Generic;
using Control;
using UnityEngine;
using UnityEngine.Playables;

namespace Interactable
{
    public class ExpeditionPanel : MonoBehaviour, IInteractable
    {
        public Transform Transform => transform;
        public string prompt;
        public string Prompt => prompt;
        public GameObject sectorSelectionPrefab;
        public Canvas targetCanvas;
        private GameObject sectorSelectionUI;
            
        public bool CanInteract(GameObject player)
        {
            return true;
        }

        public bool Interact()
        {
            if(!sectorSelectionUI)
                sectorSelectionUI = Instantiate(sectorSelectionPrefab, targetCanvas.transform);
            else
                sectorSelectionUI.SetActive(true);
            PlayerInputHandler.Instance.SwitchTo(ActionMap.UI);
            return true;
        }
    }
}
