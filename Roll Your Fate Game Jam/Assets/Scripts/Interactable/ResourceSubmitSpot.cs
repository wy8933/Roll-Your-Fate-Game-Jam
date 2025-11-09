using InventorySystem;
using UnityEngine;

namespace Interactable { 
    public class ResourceSubmitSpot : MonoBehaviour, IInteractable
    {
        public Transform Transform => transform;

        [SerializeField] private string _prompt;

        private string showedPrompt;

        public string Prompt => showedPrompt;


        public bool CanInteract(GameObject player)
        {

            if (!Inventory.Instance.ContainItem("5"))
            {
                showedPrompt = $"You don't have fuel on you \n Current Progress: {MissionManager.Instance.currentResource} / {MissionManager.Instance.resourceNeeded}";

                return true;
            }
            

            showedPrompt = $"Interact to submit fuel \n Current Progress: {MissionManager.Instance.currentResource} / {MissionManager.Instance.resourceNeeded}";
            return true;
        }

        public bool Interact()
        {
            if (Inventory.Instance.ContainItem("3"))
            {
                Inventory.Instance.RemoveItem("3");
                MissionManager.Instance.AddResource();
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}