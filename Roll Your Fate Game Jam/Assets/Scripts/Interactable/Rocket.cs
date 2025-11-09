using InventorySystem;
using UnityEngine;

namespace Interactable { 
    public class Rocket : MonoBehaviour, IInteractable
    {
        public Transform Transform => transform;

        [SerializeField] private string _prompt;

        private string showedPrompt;

        public string Prompt => showedPrompt;

        public bool CanInteract(GameObject player)
        {

            if (!MissionManager.Instance.IsAllMissionComplete())
            {
                showedPrompt = $"Current Mission Progress: \n" +
                    $"Resource Progress: {MissionManager.Instance.currentResource} / {MissionManager.Instance.resourceNeeded}" +
                    $"\n Fuel Progress: {MissionManager.Instance.currentFuel} / {MissionManager.Instance.fuelNeeded}" +
                    $"\n Antenna Progress {MissionManager.Instance.antennaFixed} / {MissionManager.Instance.antennaNeeded}";

                return true;
            }

            showedPrompt = _prompt;
            return true;
        }

        public bool Interact()
        {
            if (!MissionManager.Instance.IsAllMissionComplete()) 
            {
                return false;
            }

            GameOver();
            return true;
        }

        public void GameOver()
        {
            Debug.Log("Game is now over!");
        }
    }
}