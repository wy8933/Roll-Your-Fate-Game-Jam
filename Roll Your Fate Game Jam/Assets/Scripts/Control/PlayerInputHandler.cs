using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Control
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private InputSystem_Actions inputAction;
        public Vector2 InputVector => inputAction.Player.Move.ReadValue<Vector2>();

        public Action Interact;
        private void Awake()
        {
            inputAction = new InputSystem_Actions();
        }

        private void Start()
        {
            inputAction.Player.Interact.performed += OnInteractPressed;
        }

        private void OnDestroy()
        {
            inputAction.Player.Interact.performed -= OnInteractPressed;
        }

        public void Enable()
        {
            inputAction.Player.Enable();
        }

        public void Disable()
        {
            inputAction.Player.Disable();
        }

        public void OnInteractPressed(InputAction.CallbackContext ctx)
        {
            Interact?.Invoke();
        }
    }
}