using System;
using System.Collections.Generic;
using Template;
using UnityEngine;

namespace UI.Minimap
{
    public class MinimapManager : SingletonBehavior<MinimapManager>
    {
        GameObject[] interactables;
        [SerializeField] Transform icons;
        [SerializeField] GameObject interactableIcon;
        [SerializeField] GameObject logIcon;
        private Dictionary<GameObject, GameObject> iconDict = new Dictionary<GameObject, GameObject>();        
        private void Start()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Interactable");
            foreach (var target in targets)
            {
                GameObject icon = Instantiate(interactableIcon, icons);
                icon.transform.position = target.transform.position + transform.position;
                iconDict[target] = icon;
            }
            
            targets = GameObject.FindGameObjectsWithTag("Log");
            foreach (var target in targets)
            {
                GameObject icon = Instantiate(logIcon, icons);
                icon.transform.position = target.transform.position + transform.position;
                iconDict[target] = icon;
            }
        }

        public void Remove(GameObject removedObj)
        {
            Destroy(iconDict[removedObj]);
        }
    }
}
