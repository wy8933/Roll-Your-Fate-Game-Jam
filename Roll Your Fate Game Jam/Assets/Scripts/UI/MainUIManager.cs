using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public SettingSO settingSO;
    public GameObject settingPanel;
    public GameObject creditPanel;

    public void OpenSetting()
    {
        settingPanel.SetActive(true);
        AudioManager.Instance.Click();
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        AudioManager.Instance.Click();
    }

    public void OpenCredit()
    {
        creditPanel.SetActive(true);
        AudioManager.Instance.Click();
    }

    public void CloseCredit() 
    {
        creditPanel.SetActive(false);
        AudioManager.Instance.Click();
    }

    // public void ChangeSensitivity(float value)
    // {
    //     settingSO.UtilitySetting.mouseSensitivity = value;
    // }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseGame() 
    {
        Application.Quit();
    }

}
