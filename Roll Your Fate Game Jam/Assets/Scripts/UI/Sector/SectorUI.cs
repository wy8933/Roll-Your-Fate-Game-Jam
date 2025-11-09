using System;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sector
{
    public class SectorUI: MonoBehaviour
    {
        private Image image;
        public bool isOpened;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void UpdateHover(bool isHovered)
        {
            if (isHovered && isOpened)
            {
                image.transform.localScale = new Vector3(3,3,3);
            }
            else
            {
                image.transform.localScale = new Vector3(2.5f,2.5f,2.5f);
            }
        }
    }
}