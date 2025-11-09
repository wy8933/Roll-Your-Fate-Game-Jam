using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UI;

namespace InventorySystem
{
    public class StorageUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject _panelRoot;
        [SerializeField] private List<ItemBlockUI> _playerSlots = new();
        [SerializeField] private List<ItemBlockUI> _storageSlots = new();

        [Header("Input Actions")]
        [SerializeField] private InputActionReference _navigateAction;
        [SerializeField] private InputActionReference _confirmAction;
        [SerializeField] private InputActionReference _cancelAction;
        [SerializeField] private InputActionReference _switchPanelAction;

        [Min(1)][SerializeField] private int _columns = 5;

        private Storage _storage;
        private int _playerCursorIndex;
        private int _storageCursorIndex;
        private bool _isPlayerPanelFocused = true;

        public void Init(Storage storage)
        {
            _storage = storage;
            Hide();
            RefreshAll();
        }

        private void OnEnable()
        {
            _navigateAction?.action.Enable();
            _confirmAction?.action.Enable();
            _cancelAction?.action.Enable();
            _switchPanelAction?.action.Enable();

            if (_navigateAction != null) { _navigateAction.action.performed += OnNavigate; _navigateAction.action.canceled += OnNavigate; }
            if (_confirmAction != null) _confirmAction.action.performed += OnConfirm;
            if (_cancelAction != null) _cancelAction.action.performed += OnCancel;
            if (_switchPanelAction != null) _switchPanelAction.action.performed += OnSwitchPanel;
        }

        private void OnDisable()
        {
            if (_navigateAction != null) { _navigateAction.action.performed -= OnNavigate; _navigateAction.action.canceled -= OnNavigate; _navigateAction.action.Disable(); }
            if (_confirmAction != null) { _confirmAction.action.performed -= OnConfirm; _confirmAction.action.Disable(); }
            if (_cancelAction != null) { _cancelAction.action.performed -= OnCancel; _cancelAction.action.Disable(); }
            if (_switchPanelAction != null) { _switchPanelAction.action.performed -= OnSwitchPanel; _switchPanelAction.action.Disable(); }
        }

        public void Show()
        {
            _isPlayerPanelFocused = true;
            _playerCursorIndex = 0;
            _storageCursorIndex = 0;

            if (_panelRoot) _panelRoot.SetActive(true);
            RefreshAll();
            UpdateHighlights();
        }

        public void Hide()
        {
            if (_panelRoot) _panelRoot.SetActive(false);
            SetSelectionVisuals(_playerSlots, -1);
            SetSelectionVisuals(_storageSlots, -1);
        }

        private void OnNavigate(InputAction.CallbackContext context)
        {
            var axis = context.ReadValue<Vector2>();
            if (!context.performed) return;

            const float deadzone = 0.3f;
            int stepX = axis.x < -deadzone ? -1 : (axis.x > deadzone ? 1 : 0);
            int stepY = axis.y < -deadzone ? -1 : (axis.y > deadzone ? 1 : 0);

            if (_isPlayerPanelFocused)
            {
                int last = Mathf.Max(0, _playerSlots.Count - 1);
                _playerCursorIndex = MoveCursor(_playerCursorIndex, stepX, stepY, _columns, last);
            }
            else
            {
                int last = Mathf.Max(0, _storageSlots.Count - 1);
                _storageCursorIndex = MoveCursor(_storageCursorIndex, stepX, stepY, _columns, last);
            }

            UpdateHighlights();
        }



        private void OnConfirm(InputAction.CallbackContext _)
        {
            if (_storage == null) return;

            if (_isPlayerPanelFocused)
            {
                if (_storage.TryStoreFromPlayer(_playerCursorIndex))
                    _playerCursorIndex = Mathf.Clamp(_playerCursorIndex, 0, Mathf.Max(0, Inventory.Instance.items.Count - 1));
            }
            else
            {
                if (_storage.TryTakeToPlayer(_storageCursorIndex))
                    _storageCursorIndex = Mathf.Clamp(_storageCursorIndex, 0, Mathf.Max(0, _storage.Items.Count - 1));
            }

            RefreshAll();
            UpdateHighlights();
        }

        private void OnCancel(InputAction.CallbackContext _) => _storage?.Close();

        private void OnSwitchPanel(InputAction.CallbackContext _)
        {
            _isPlayerPanelFocused = !_isPlayerPanelFocused;
            UpdateHighlights();
        }

        private void RefreshAll()
        {
            var inv = Inventory.Instance;
            if (inv != null) RenderSlots(_playerSlots, inv.items, inv.maxItemCount);
            if (_storage != null) RenderSlots(_storageSlots, _storage.Items, _storage.Capacity);
        }

        private void RenderSlots(List<ItemBlockUI> slots, IReadOnlyList<ItemSO> sourceItems, int capacity)
        {
            if (slots == null) return;

            int fillCount = Mathf.Min(sourceItems.Count, slots.Count);
            for (int i = 0; i < fillCount; i++)
                slots[i].SetIcon(sourceItems[i] ? sourceItems[i].itemIcon : null);

            for (int i = fillCount; i < Mathf.Min(capacity, slots.Count); i++)
                slots[i].SetIconAlphaZero();

            for (int i = capacity; i < slots.Count; i++)
                slots[i].SetIconAlphaZero();
        }

        private void UpdateHighlights()
        {
            int playerMaxIndex = Mathf.Max(0, _playerSlots.Count - 1);
            int storageMaxIndex = Mathf.Max(0, _storageSlots.Count - 1);

            _playerCursorIndex = Mathf.Clamp(_playerCursorIndex, 0, playerMaxIndex);
            _storageCursorIndex = Mathf.Clamp(_storageCursorIndex, 0, storageMaxIndex);

            SetSelectionVisuals(_playerSlots, _isPlayerPanelFocused ? _playerCursorIndex : -1);
            SetSelectionVisuals(_storageSlots, !_isPlayerPanelFocused ? _storageCursorIndex : -1);
        }


        private static void SetSelectionVisuals(List<ItemBlockUI> slots, int selectedIndex)
        {
            if (slots == null) return;
            for (int i = 0; i < slots.Count; i++)
                slots[i].SetSelected(i == selectedIndex);
        }

        private static int MoveCursor(int index, int stepX, int stepY, int columns, int maxIndex)
        {
            int row = index / columns;
            int col = index % columns;

            col += stepX;
            row -= stepY;

            col = Mathf.Clamp(col, 0, columns - 1);

            int target = row * columns + col;
            return Mathf.Clamp(target, 0, Mathf.Max(0, maxIndex));
        }

    }
}
