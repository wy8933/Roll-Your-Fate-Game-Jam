using System;
using System.Collections;
using UnityEngine;
using InventorySystem;
using Minigame;
using UnityEngine.Events;

public class MinigameLauncher : MonoBehaviour, IInteractable
{
    public Transform Transform => transform;

    [SerializeField] private string _prompt = "Interact";
    public string Prompt => _prompt;

    public GameObject minigamePrefab;
    private MinigameController minigame;

    private Canvas targetCanvas;

    public UnityEvent MinigameCleared;

    bool isCleared = false;

    private void Awake()
    {
        if(targetCanvas == null)
            targetCanvas = GameObject.FindWithTag("Minigame Canvas").GetComponent<Canvas>();
    }

    public void Start()
    {
        MinigameCleared.AddListener(()=>{Debug.Log("MinigameCleared!");});
    }

    private void OnDisable()
    {
        MinigameCleared.RemoveAllListeners();
    }

    public bool CanInteract(GameObject player)
    {
        return !isCleared;
    }

    public bool Interact()
    {
        StartCoroutine(Launch());
        return true;
    }

    IEnumerator Launch()
    {
        if (minigame == null)
            minigame = Instantiate(minigamePrefab, targetCanvas.transform)?.GetComponent<MinigameController>();
        minigame.Launch();
        yield return new WaitUntil(() => minigame.Status != Status.Progressing);
        if (minigame.Status == Status.Cleared)
        {
            isCleared = true;   
            MinigameCleared?.Invoke();
        }
    }
}