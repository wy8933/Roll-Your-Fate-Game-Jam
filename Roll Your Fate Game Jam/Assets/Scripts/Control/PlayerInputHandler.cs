using System;
using Template;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Control
{
    public class PlayerInputHandler : SingletonBehavior<PlayerInputHandler>
    {
        private InputSystem_Actions inputAction;
        public Vector2 InputVector => inputAction.Player.Move.ReadValue<Vector2>();

        public Action Interact;
        
        protected override void Awake()
        {
            base.Awake();
            inputAction = new InputSystem_Actions();
        }

        void Start()
        {
            inputAction.Player.Interact.performed += OnInteractPressed;
        }

        void OnDestroy()
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

        protected void OnInteractPressed(InputAction.CallbackContext ctx)
        {
            Interact?.Invoke();
            Debug.Log("Interact");
        }
    }
}