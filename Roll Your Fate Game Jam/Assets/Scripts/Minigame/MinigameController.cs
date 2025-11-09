using System;
using Control;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Minigame
{
    public class MinigameController : MonoBehaviour
    {
        // private MinigameLauncher source;
        private Status status;
        public Status Status => status;
        
        public bool ResetAfterEveryRun = false;

        public GameObject firstSelectedButton;
        
        bool isInitialized = false;
        
        protected float timer = 0f;
        
        protected AudioSource audioSource;
        protected AudioClip winSFX;

        protected void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        protected virtual void Update()
        {
            timer += Time.deltaTime;
        }

        public virtual void Launch()
        {
            gameObject.SetActive(true);
            PlayerInputHandler.Instance.SwitchTo(ActionMap.UI);
            PlayerInputHandler.Instance.Navigate += OnNavigate;
            PlayerInputHandler.Instance.Click += OnClick;
            PlayerInputHandler.Instance.RightClick += OnRightClick;
            if(!isInitialized)
                Initialize();
            if(firstSelectedButton != null)
                EventSystem.current.SetSelectedGameObject(firstSelectedButton);
            status = Status.Progressing;
        }

        protected virtual void Initialize()
        {
            isInitialized = true;
        }
        
        void OnDisable()
        {
            PlayerInputHandler.Instance.SwitchTo(ActionMap.Player);
            PlayerInputHandler.Instance.Navigate -= OnNavigate;
            PlayerInputHandler.Instance.Click -= OnClick;
            PlayerInputHandler.Instance.RightClick -= OnRightClick;
        }
        
        // These are not necessary if only buttons are used. The EventSystem will handle the button selection and click
        public virtual void OnNavigate(Vector2 dir)
        {
            Debug.Log($"Navigate:{dir}");
        }

        public virtual void OnClick()
        {
            Debug.Log($"Click");
        }
        
        public virtual void OnRightClick()
        {
            Debug.Log($"RightClick");
        }


        public virtual void GameAbort()
        {
            status = Status.Aborted;
            if (ResetAfterEveryRun)
                Reset();
            gameObject.SetActive(false);
        }

        public virtual void GameClear()
        {
            status =  Status.Cleared;
            gameObject.SetActive(false);
        }

        public virtual void Reset()
        {
            timer = 0f;
        }
    }

    public enum Status
    {
        Progressing,
        Cleared,
        Aborted
    }
}