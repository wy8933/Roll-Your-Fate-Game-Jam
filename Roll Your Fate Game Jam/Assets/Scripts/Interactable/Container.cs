using System.Collections.Generic;
using UnityEngine;

namespace Interactable { 
    public class Container : MonoBehaviour, IInteractable
    {
        public Transform Transform => gameObject.transform;

        [SerializeField] private string _prompt;
        public string Prompt => _prompt;


        public List<List<ItemSO>> itemGrid =  new List<List<ItemSO>>(); 

        public bool CanInteract(GameObject player)
        {
            return true;
        }

        public bool Interact()
        {
            return false;
        }
    }
}