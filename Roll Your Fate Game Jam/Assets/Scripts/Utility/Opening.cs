using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadGame());
    }

    public IEnumerator LoadGame()
    {
        VideoManager.Instance.PlayStartClip();
        yield return new WaitForSeconds(22);
        SceneManager.LoadScene(2);
    }
}
