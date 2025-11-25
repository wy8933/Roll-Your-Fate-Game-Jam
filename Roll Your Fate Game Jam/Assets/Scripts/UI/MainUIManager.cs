using Control;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public SettingSO settingSO;
    public GameObject settingPanel;
    public GameObject creditPanel;

    public GameObject mainMenuFirstButton;
    public GameObject settingFirstButton;
    public GameObject creditFirstButton;

    public GameObject mainMenuButtons;
    public GameObject title;

    public void OpenSetting()
    {
        settingPanel.SetActive(true);
        AudioManager.Instance.Click();
        mainMenuButtons.SetActive(false);
        EventSystem.current.SetSelectedGameObject(settingFirstButton);
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        AudioManager.Instance.Click();
        mainMenuButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    public void OpenCredit()
    {
        creditPanel.SetActive(true);
        AudioManager.Instance.Click();
        mainMenuButtons.SetActive(false);
        EventSystem.current.SetSelectedGameObject(creditFirstButton);
    }

    public void CloseCredit() 
    {
        creditPanel.SetActive(false);
        AudioManager.Instance.Click();
        mainMenuButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    // public void ChangeSensitivity(float value)
    // {
    //     settingSO.UtilitySetting.mouseSensitivity = value;
    // }


    public void CloseGame() 
    {
        Application.Quit();
    }
    public void StartGame()
    {
        GetComponent<PlayerInputHandler>().SwitchTo(ActionMap.Player);
        VideoManager.Instance.PlayStartClip();
        AudioManager.Instance.StopMusic();
        mainMenuButtons.SetActive(false);
        title.SetActive(false);
        StartCoroutine(LoadMain());
    }

    private IEnumerator LoadMain()
    {
        Debug.Log("Start");
        yield return new WaitForSeconds(20); 
        Debug.Log(1);
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }

}
