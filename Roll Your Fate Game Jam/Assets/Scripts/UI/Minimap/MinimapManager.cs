using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Minimap
{
    public class MinimapManager : MonoBehaviour
    {
        GameObject[] interactables;
        [SerializeField] Transform icons;
        [SerializeField] GameObject interactableIcon;
        [SerializeField] GameObject logIcon;
        
        private void Start()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Interactable");
            foreach (var target in targets)
            {
                GameObject icon = Instantiate(interactableIcon, icons);
                icon.transform.position = target.transform.position + transform.position;
            }
            
            targets = GameObject.FindGameObjectsWithTag("Log");
            foreach (var target in targets)
            {
                GameObject icon = Instantiate(logIcon, icons);
                icon.transform.position = target.transform.position + transform.position;
            }
        }
    }
}
