using System;
using Template;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Control
{
    public class PlayerInputHandler : SingletonBehavior<PlayerInputHandler>
    {
        private InputSystem_Actions inputAction;
        public ActionMap currentActionMap;
        public Vector2 InputVector => inputAction.Player.Move.ReadValue<Vector2>();

        #region ActionMap.Player
        public Action Interact;
        public Action ToggleHUD;
        #endregion
        
        #region ActionMap.UI
        public Action<Vector2> Navigate;
        public Action<Vector2> Look;
        public Action<Vector2> Point;
        public Action Click;
        public Action RightClick;
        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            inputAction = new InputSystem_Actions();
        }

        void OnEnable()
        {
            inputAction.Player.Interact.performed += OnInteractPressed;
            inputAction.Player.ToggleHUD.performed += OnToggleHUDPressed;
            inputAction.UI.Navigate.performed += OnNavigate;
            inputAction.UI.Look.performed += OnLook;
            inputAction.UI.Point.performed += OnPoint;
            inputAction.UI.Click.performed += OnUIClick;
            inputAction.UI.RightClick.performed += OnUIRightClick;
        }

        void OnDisable()
        {
            inputAction.Player.Interact.performed -= OnInteractPressed;
            inputAction.Player.ToggleHUD.performed -= OnToggleHUDPressed;
            inputAction.UI.Navigate.performed -= OnNavigate;
            inputAction.UI.Point.performed -= OnPoint;
            inputAction.UI.Click.performed -= OnUIClick;
            inputAction.UI.RightClick.performed -= OnUIRightClick;
        }

        public void Enable()
        {
            switch (currentActionMap) // Enable one Action map
            {
                case  ActionMap.Player:
                    inputAction.Player.Enable();
                    break;
                case ActionMap.UI:
                    inputAction.UI.Enable();
                    break;
            }
        }

        public void Disable()
        {
            inputAction.Disable();
        }

        public void SwitchTo(ActionMap newMap)
        {
            currentActionMap = newMap;
            Disable(); // Diable All Action map
            switch (currentActionMap) // Enable one Action map
            {
                case  ActionMap.Player:
                    inputAction.Player.Enable();
                    break;
                case ActionMap.UI:
                    inputAction.UI.Enable();
                    break;
            }
        }

        protected void OnInteractPressed(InputAction.CallbackContext ctx)
        {
            Interact?.Invoke();
            // Debug.Log("Interact Pressed");
        }

        protected void OnToggleHUDPressed(InputAction.CallbackContext ctx)
        {
            ToggleHUD?.Invoke();
        }
        
        protected void OnNavigate(InputAction.CallbackContext ctx)
        {
            Vector2 value = ctx.ReadValue<Vector2>();
            Navigate?.Invoke(value);
        }
        
        protected void OnPoint(InputAction.CallbackContext ctx)
        {
            Vector2 value = ctx.ReadValue<Vector2>();
            Point?.Invoke(value);
        }
        
        protected void OnLook(InputAction.CallbackContext ctx)
        {
            Vector2 value = ctx.ReadValue<Vector2>();
            Look?.Invoke(value);
        }
        
        protected void OnUIClick(InputAction.CallbackContext ctx)
        {
            Click?.Invoke();
        }

        protected void OnUIRightClick(InputAction.CallbackContext ctx)
        {
            RightClick?.Invoke();
        }
    }

    public enum ActionMap
    {
        Player,
        UI
    }
}