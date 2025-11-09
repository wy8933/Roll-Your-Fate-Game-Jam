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
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            _instance = GetComponent<T>();
        }
    }
    
}