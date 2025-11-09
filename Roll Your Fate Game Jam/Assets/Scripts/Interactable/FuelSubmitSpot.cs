using InventorySystem;
using UnityEngine;

namespace Interactable
{
    public class FuelSubmitSpot : MonoBehaviour, IInteractable
    {
        public Transform Transform => transform;

        [SerializeField] private string _prompt;

        private string showedPrompt;

        public string Prompt => showedPrompt;


        public bool CanInteract(GameObject player)
        {

            if (!Inventory.Instance.ContainItem("5"))
            {
                showedPrompt = $"You don't have fuel on you \n Current Progress: {MissionManager.Instance.currentFuel} / {MissionManager.Instance.fuelNeeded}";

                return true;
            }


            showedPrompt = $"Interact to submit fuel \n Current Progress: {MissionManager.Instance.currentFuel} / {MissionManager.Instance.fuelNeeded}";
            return true;
        }

        public bool Interact()
        {
            if (Inventory.Instance.ContainItem("5"))
            {
                Inventory.Instance.RemoveItem("5");
                MissionManager.Instance.AddFuel();
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}