using System;
using UnityEngine;
using InventorySystem;

namespace Interactable { 
    public class Atenna : MonoBehaviour, IInteractable
    {
        public Transform Transform => transform;

        [SerializeField] private string _prompt;

        public string Prompt => _prompt;

        public bool isFixed = false;

        public string[] neededItemList = new string[3];

        public GameObject _promptUI;

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
                    if (_promptUI != null)
                    {
                        _promptUI.gameObject.SetActive(true);
                        _promptUI.gameObject.transform.position = transform.position + new Vector3(0,2,0);
                    }
                    else
                    {
                        _promptUI.gameObject.SetActive(false);
                    }

                    return false;
                }
            }

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