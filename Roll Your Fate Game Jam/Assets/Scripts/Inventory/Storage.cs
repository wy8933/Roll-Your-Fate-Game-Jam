using System.Collections.Generic;
using UnityEngine;
using Control;

namespace InventorySystem
{
    public class Storage : MonoBehaviour, IInteractable
    {
        public Transform Transform => transform;

        [SerializeField] private string _prompt = "Open Storage";
        public string Prompt => _prompt;

        [Min(1)][SerializeField] private int _capacity = 12;
        [SerializeField] private List<ItemSO> _items = new();

        [SerializeField] private StorageUI _storageUI;

        private bool _open;

        public IReadOnlyList<ItemSO> Items => _items;
        public int Capacity => _capacity;

        public bool CanInteract(GameObject player) => !_open;

        public bool Interact()
        {
            if (_open) return false;
            if (_storageUI == null) return false;

            _storageUI.Init(this);
            _storageUI.gameObject.SetActive(true);
            _open = true;
            PlayerInputHandler.Instance.SwitchTo(ActionMap.UI);

            return true;
        }

        public void Close()
        {
            if (!_open) return;
            _storageUI?.gameObject.SetActive(false);
            _open = false;
            PlayerInputHandler.Instance.SwitchTo(ActionMap.Player);
        }

        public bool TryStoreFromPlayer(int playerIndex)
        {
            if (Inventory.Instance == null) return false;
            if (playerIndex < 0 || playerIndex >= Inventory.Instance.items.Count) return false;
            if (_items.Count >= _capacity) return false;

            var item = Inventory.Instance.items[playerIndex];
            if (item == null) return false;

            _items.Add(item);
            Inventory.Instance.RemoveItem(item.itemID);
            return true;
        }

        public bool TryTakeToPlayer(int storageIndex)
        {
            if (Inventory.Instance == null) return false;
            if (storageIndex < 0 || storageIndex >= _items.Count) return false;
            if (Inventory.Instance.items.Count >= Inventory.Instance.maxItemCount) return false;

            var item = _items[storageIndex];
            if (item == null) return false;

            if (!Inventory.Instance.AddItem(item)) return false;
            _items.RemoveAt(storageIndex);
            return true;
        }
    }
}
