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
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }

    public void OpenCredit()
    {
        creditPanel.SetActive(true);
    }

    public void CloseCredit() 
    {
        creditPanel.SetActive(false);
    }

    public void ChangeVolume(float value) 
    {
        settingSO.UtilitySetting.audioVolume = value;
    }

    public void ChangeSensitivity(float value)
    {
        settingSO.UtilitySetting.mouseSensitivity = value;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseGame() 
    {
        Application.Quit();
    }

}
