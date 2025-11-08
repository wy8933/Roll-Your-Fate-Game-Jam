using System.Collections.Generic;
using Control;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private List<IInteractable> _interactableInRange = new List<IInteractable>();
    private IInteractable _current;


    private void OnEnable()
    {
        PlayerInputHandler.Instance.Interact += OnInteract;
    }

    private void OnDisable()
    {
        PlayerInputHandler.Instance.Interact -= OnInteract;
    }

    private void Update()
    {
        _current = FindBestTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
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
        if (_current != null)
        {
            _current.Interact();
            _interactableInRange.Remove(_current);
        }
    }

    private IInteractable FindBestTarget()
    {
        if (_interactableInRange.Count == 0) return null;

        IInteractable best = null;
        float bestSqr = float.MaxValue;
        Vector3 here = transform.position;

        for (int n = _interactableInRange.Count - 1; n >= 0; n--)
        {
            IInteractable i = _interactableInRange[n];
            if (i == null) { _interactableInRange.RemoveAt(n); continue; }
            if (!i.CanInteract(gameObject)) continue;

            float distance = (i.Transform.position - here).sqrMagnitude;
            if (distance < bestSqr) { bestSqr = distance; best = i; }
        }
        return best;
    }
}
