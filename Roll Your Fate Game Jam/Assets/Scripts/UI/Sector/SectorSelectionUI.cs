using System;
using System.Collections.Generic;
using Control;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sector
{
    public class SectorSelectionUI : MonoBehaviour
    {
        List<SectorUI> sectors = new List<SectorUI>();
        void OnEnable()
        {
            PlayerInputHandler.Instance.SwitchTo(ActionMap.UI);
            PlayerInputHandler.Instance.Point += OnPoint;
        }

        void OnDisable()
        {
            PlayerInputHandler.Instance.Point += OnPoint;
        }

        private int targetSectorID = -1;
        private void OnPoint(Vector2 dir)
        {
            if (dir.magnitude > 0.2)
            {
                float rotation = Mathf.Atan2(dir.y, dir.x);
                targetSectorID = Mathf.RoundToInt(Mathf.Floor(rotation / (Mathf.PI / 3)));
                sectors[targetSectorID].Select();
            }
            else
                targetSectorID = -1;
        }

        private void OnClick()
        {
            if (targetSectorID != -1)
            {
                
            }
        }
    }
}