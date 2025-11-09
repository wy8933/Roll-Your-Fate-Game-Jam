using System;
using System.Collections.Generic;
using Control;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sector
{
    public class SectorSelectionUI : MonoBehaviour
    {
        [SerializeField]
        private Transform sectorsParent;
        List<SectorUI> sectors = new List<SectorUI>();

        private void Awake()
        {
            for (int i = 0; i < sectorsParent.childCount; i++)
            {
                sectors.Add(sectorsParent.GetChild(i).GetComponent<SectorUI>());
            }
        }

        void OnEnable()
        {
            PlayerInputHandler.Instance.SwitchTo(ActionMap.UI);
            PlayerInputHandler.Instance.Point += OnPoint;
            PlayerInputHandler.Instance.Click += OnClick;
        }

        void OnDisable()
        {
            PlayerInputHandler.Instance.Point -= OnPoint;
            PlayerInputHandler.Instance.Click -= OnClick;
        }

        private int targetSectorID = -1;
        private void OnPoint(Vector2 dir)
        {
            if (dir.magnitude > 0.2)
            {
                if (targetSectorID != -1)
                    sectors[targetSectorID].UpdateHover(false);
                float rotation = Mathf.Atan2(dir.y, dir.x);
                if (rotation < 0)
                    rotation += Mathf.PI * 2;
                Debug.Log(rotation / (Mathf.PI / 3));
                targetSectorID = Mathf.RoundToInt(Mathf.Floor(rotation / (Mathf.PI / 3)));
                sectors[targetSectorID].UpdateHover(true);
                Debug.Log(targetSectorID);
            }
            else
            {
                if (targetSectorID != -1)
                    sectors[targetSectorID].UpdateHover(false);
                targetSectorID = -1;
            }
        }

        private void OnClick()
        {
            Debug.Log($"GO:{targetSectorID}");
            if (targetSectorID != -1 && sectors[targetSectorID].isOpened)
            {
                gameObject.SetActive(false);
                PlayerInputHandler.Instance.SwitchTo(ActionMap.Player);
                GameManager.Instance.SetOff(targetSectorID);
            }
        }

        private void OnRightClick()
        {
            PlayerInputHandler.Instance.SwitchTo(ActionMap.Player);
            gameObject.SetActive(false);
        }
    }
}