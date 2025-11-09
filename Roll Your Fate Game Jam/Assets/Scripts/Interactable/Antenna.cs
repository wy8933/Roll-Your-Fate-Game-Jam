using System;
using UnityEngine;
using InventorySystem;

namespace Interactable { 
    public class Atenna : MonoBehaviour, IInteractable
    {
        public Transform Transform => transform;

        [SerializeField] private string _prompt;

        private string showedPrompt;

        public string Prompt => showedPrompt;

        public bool isFixed = false;

        public string[] neededItemList = new string[3];


        public bool CanInteract(GameObject player)
        {
            if (isFixed) 
            {
                return false;
            }

            foreach (string itemID in neededItemList) 
            {
                if (!Inventory.Instance.ContainItem(itemID))
                {
                    showedPrompt = "You will need 1 Atenna, 1 Chip and 1 Battery";

                    return true;
                }
            }

            showedPrompt = _prompt;
            return true;
        }

        public bool Interact()
        {
            foreach (string itemID in neededItemList)
            {
                if (Inventory.Instance.ContainItem(itemID))
                {
                    Inventory.Instance.RemoveItem(itemID);
                }
                else 
                {
                    return false;
                }
            }

            isFixed = true;
            return true;
        }
    }
}