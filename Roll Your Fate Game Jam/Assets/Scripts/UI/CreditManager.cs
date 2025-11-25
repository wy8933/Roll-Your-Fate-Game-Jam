using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private string _creditsSceneName = "CreditScene";

    public void ShowCredits()
    {
        SceneManager.LoadScene(_creditsSceneName);
    }

    public void CloseCredits()
    {
        SceneManager.LoadScene("Start_Page");
    }
}
