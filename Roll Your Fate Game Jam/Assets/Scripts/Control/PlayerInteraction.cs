using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private List<IInteractable> _interactableInRange = new List<IInteractable>();
    [SerializeField] private TMP_Text _promptUI;
    private IInteractable _current;

    [SerializeField] private InputActionReference interactAction;

    private void OnEnable()
    {
        interactAction.action.Enable();
        interactAction.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        interactAction.action.Disable();
        interactAction.action.performed -= OnInteract;
    }

    private void Update()
    {
        _current = FindBestTarget();
        if (_current != null)
        {
            _promptUI.gameObject.SetActive(true);
            _promptUI.text = _current.Prompt;
        }
        else
        {
            _promptUI.gameObject.SetActive(false);
        }
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


    public void OnInteract(InputAction.CallbackContext context) 
    {
        _current.Interact();
    }

    private IInteractable FindBestTarget()
    {
        Debug.Log(_interactableInRange.Count);
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
        Debug.Log("Best is " + best);
        return best;
    }
}
