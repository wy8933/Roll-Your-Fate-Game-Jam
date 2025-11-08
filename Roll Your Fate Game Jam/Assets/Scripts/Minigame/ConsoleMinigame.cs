using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame
{
    public class ConsoleMinigame : MinigameController
    {
        List<int> commands = new List<int>();
        [SerializeField] protected Transform content;
        protected List<Transform> slots = new List<Transform>();
        [SerializeField] protected List<GameObject> commandsPrefabs;
        List<Image> commandsUIs = new List<Image>();
        public int numberOfCommands;
        
        AudioSource audioSource;
        AudioClip inputSFX;
        AudioClip winSFX;
        AudioClip loseSFX;
        
        int index;
        private bool isInputing;
        
        public override void Launch()
        {
            base.Launch();
            isInputing = true;
            index = 0;
            for (int i = 0; i < numberOfCommands; i++)
            {
                commands.Add(Random.Range(0, 4));
                commandsUIs.Add(Instantiate(commandsPrefabs[commands[i]], slots[i])?.GetComponent<Image>());
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            audioSource = GetComponent<AudioSource>();
            for (int i = 0; i < content.childCount; i++)
                slots.Add(content.GetChild(i));
        }

        public override void OnNavigate(Vector2 dir)
        {
            base.OnNavigate(dir);
            if (dir.magnitude < 0.5f)
            {
                isInputing = true;
                return;
            }

            if (!isInputing)
                return;
            isInputing = false;
            for (int i = 0; i < 4; i++)
            {
                Vector2 vec = new Vector2(Mathf.Cos(i * Mathf.PI / 2), Mathf.Sin(i * Mathf.PI / 2));
                if ((vec - dir).magnitude < 0.1f)
                {
                    InputDirection(i);
                    return;
                }
            }
        }

        public override void OnRightClick()
        {
            base.OnRightClick();
            GameAbort();
        }

        public void InputDirection(int directionID)
        {
            Debug.Log($"Input: {directionID}");
            if (commands[index] == directionID)
            {
                commandsUIs[index].color = Color.green;
                index++;
                audioSource.PlayOneShot(inputSFX);
                if (index == numberOfCommands)
                {
                    StartCoroutine(Win());
                }
            }
            else
            {
                StartCoroutine(Error());
            }
        }
        
        IEnumerator Win()
        {
            audioSource.PlayOneShot(winSFX);
            yield return new WaitForSeconds(2f);
            GameClear();
        }
        
        IEnumerator Error()
        {
            audioSource.PlayOneShot(winSFX);
            for (int i = index; i < numberOfCommands; i++)
                commandsUIs[i].color = Color.red;
            yield return new WaitForSeconds(1f);
            GameAbort();
        }

        public override void Reset()
        {
            base.Reset();
            commands.Clear();
            foreach (var UI in commandsUIs)
            {
                Destroy(UI.gameObject);
            }
            commandsUIs.Clear();
        }
    }
}