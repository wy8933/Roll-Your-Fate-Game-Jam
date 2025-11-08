using System.Collections.Generic;
using UnityEngine;

namespace Control { 
    public class PlayerInteraction : MonoBehaviour
    {
        private List<IInteractable> _interactableInRange = new List<IInteractable>();
        private IInteractable _current;

        private void Start()
        {
            PlayerInputHandler.Instance.Interact += OnInteract;
        }

        private void OnDestroy()
        {
            PlayerInputHandler.Instance.Interact -= OnInteract;
        }

        private void Update()
        {
            _current = FindBestTarget();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the entered object is a interactable object, if yes, add it to the _interactableInRange list
            if (other.TryGetComponent<IInteractable>(out var interactable)) 
            {
                if (interactable != null && !_interactableInRange.Contains(interactable))
                {
                    _interactableInRange.Add(interactable);
                } 
            }
            else
            {
                interactable = other.GetComponentInParent<IInteractable>();
                if (interactable != null)
                {
                    _interactableInRange.Add(interactable);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // Check if the exited object is a interactable object, if yes, remove it from the _interactableInRange list
            if (other.TryGetComponent<IInteractable>(out var interactable))
            {
                if (interactable != null && _interactableInRange.Contains(interactable))
                {

                    _interactableInRange.Remove(interactable);
                }
            }
            else
            {
                interactable = other.GetComponentInParent<IInteractable>();
                if (interactable != null)
                {
                    _interactableInRange.Remove(interactable);
                }
            }
        }

        public void OnInteract() 
        {
            if(_current != null)
                _current.Interact();
        }

        /// <summary>
        /// Find the closest interactable from the player
        /// </summary>
        /// <returns>the closest interactable</returns>
        private IInteractable FindBestTarget()
        {
            if (_interactableInRange.Count == 0) return null;

            IInteractable best = null;
            float bestSqr = float.MaxValue;
            Vector3 here = transform.position;

            //iterate through all the interactables
            for (int n = _interactableInRange.Count - 1; n >= 0; n--)
            {
                // check if the interactable's interation condition is met
                IInteractable i = _interactableInRange[n];
                if (i == null) { _interactableInRange.RemoveAt(n); continue; }
                if (!i.CanInteract(gameObject)) continue;

                // check if it's the closest one from the player
                float distance = (i.Transform.position - here).sqrMagnitude;
                if (distance < bestSqr) { bestSqr = distance; best = i; }
            }
            return best;
        }
    }
}