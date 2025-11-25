using Control;
using Game;
using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseMenu;

    public void OnEnable()
    {
        PlayerInputHandler.Instance.TogglePause += TogglePause;
    }

    public void OnDisable()
    {
        PlayerInputHandler.Instance.TogglePause -= TogglePause;
    }

    public void TogglePause() 
    {
        Time.timeScale = Convert.ToInt32(isPaused);
        pauseMenu.SetActive(!isPaused);
        isPaused = !isPaused;
    }

}
