using System;
using Control;
using UnityEngine;

namespace UI
{
    public class SectorSelectionUI : MonoBehaviour
    {
        void OnEnable()
        {
            PlayerInputHandler.Instance.SwitchTo(ActionMap.UI);
            PlayerInputHandler.Instance.Navigate += OnNavigate;
        }

        void OnDisable()
        {
            PlayerInputHandler.Instance.Navigate += OnNavigate;
        }

        private void OnNavigate(Vector2 dir)
        {
            float rotation = Mathf.Atan2(dir.y, dir.x);
            int sectorID = Mathf.RoundToInt(Mathf.Floor(rotation / (Mathf.PI / 3)));
            Debug.Log(sectorID);
        }
    }
}