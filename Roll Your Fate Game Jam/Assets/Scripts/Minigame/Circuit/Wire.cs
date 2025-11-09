using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Circuit
{
    public class Wire : MonoBehaviour
    {
        [SerializeField] private int connection; // ULDR
        public int Connection => connection;
        private Image image;
        private float rotateSpeed = 90;
        bool isRotating = false;
        private void Awake()
        {
            image = GetComponent<Image>();
        }
        
        public void Rotate()
        {
            if (!isRotating)
            {
                connection = (connection << 1) & ((1 << 4)-1) | ((connection & (1 << 3)) > 0 ? 1 : 0);
                StartCoroutine(Rotation());
            }
        }

        IEnumerator Rotation()
        {
            isRotating = true;
            float initRotation = image.transform.rotation.eulerAngles.z;
            float rotation = 0;
            while (rotation < 90)
            {
                rotation = Mathf.Clamp(rotation + rotateSpeed * Time.deltaTime, 0, 90);
                image.transform.rotation = Quaternion.Euler(0, 0, initRotation + rotation);
                yield return null;
            }
            isRotating = false;
        }
        
    }
}
