using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public class SingletonBehavior<T> : MonoBehaviour where T : new()
    {
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject(typeof(T).Name);
                    gameObject.AddComponent(typeof(T));
                    if(Application.isPlaying)
                        DontDestroyOnLoad(gameObject);
                    _instance = gameObject.GetComponent<T>();
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            // Debug.LogWarning("Awake");
            if (_instance == null)
            {
                DontDestroyOnLoad(gameObject);
                _instance = GetComponent<T>();
            }
            else
                Destroy(gameObject);
        }
    }
    
}