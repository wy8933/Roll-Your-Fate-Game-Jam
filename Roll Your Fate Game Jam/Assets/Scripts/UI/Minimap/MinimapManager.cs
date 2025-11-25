using System;
using System.Collections.Generic;
using Control;
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
        [SerializeField] GameObject baseIcon;
        GameObject baseIconGO;
        [SerializeField] private Transform baseTransform;
        [SerializeField] private Transform playerTransform;
        [SerializeField] float minimapRaidus;
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
            
            baseIconGO = Instantiate(baseIcon, icons);
            baseIconGO.transform.position = baseTransform.position + transform.position;
        }

        private void Update()
        {
            Debug.Log($"{playerTransform.position}, {(baseTransform.position - playerTransform.position).magnitude}");
            if ((baseTransform.position - playerTransform.position).magnitude < minimapRaidus)
            {
                baseIconGO.SetActive(false);
            }
            else
            {
                baseIconGO.SetActive(true);
                baseIconGO.transform.position = playerTransform.position + transform.position + Vector3.ClampMagnitude(baseTransform.position - playerTransform.position, minimapRaidus);
                Vector3 right = (baseTransform.position - playerTransform.position).normalized;
                right.y = 0;
                Vector3 forward = new Vector3(0, 1f, 0);
                baseIconGO.transform.rotation = Quaternion.LookRotation(forward, Vector3.Cross(right, forward));
            }
        }

        public void Remove(GameObject removedObj)
        {
            Destroy(iconDict[removedObj]);
        }
    }
}
