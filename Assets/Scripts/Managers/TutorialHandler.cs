using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject startButton, toolPanel, statsPanel, computerPanel;
    [SerializeField] private GameObject buildButton, moveButton, navigateButton, demolishButton, inventoryButton;

    [SerializeField] private GameObject[] objectsToDisable;

    [SerializeField] private TutorialSequence[] tutorialSequences;
    
    private TextSpawner _spawner;
    
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private GameObject tutorialDialogueWindow;
    [SerializeField] private GameObject interactionBlocker;
    [SerializeField] private GameObject nextDialogueButton;

    private Coroutine _currentCoroutine;

    private int _currentSequenceIndex;
    private int _currentDialogueIndex;
    
    public static bool InDialogue { get; private set; }

    private void Start()
    {
        _spawner = GetComponent<TextSpawner>();
        _spawner.SequenceStarted += delegate { ChangeNextButtonState(false); };
        _spawner.SequenceEnded += delegate { ChangeNextButtonState(true); };
        
        DisableAllGameCanvasMenus();
        
        _currentSequenceIndex = 0;
        _currentDialogueIndex = 0;
        
        EnableAndStartNextSequence();
    }

    private void ChangeNextButtonState(bool state)
    {
        nextDialogueButton.SetActive(state);
    }

    public void StartNextDialogue()
    {
        StopCoroutine(_currentCoroutine);
        InDialogue = true;
        if (_currentDialogueIndex >= tutorialSequences[_currentSequenceIndex].dialogueInSequence.Length - 1)
        {
            tutorialDialogueWindow.SetActive(false);
            interactionBlocker.SetActive(false);
            InDialogue = false;
            tutorialSequences[_currentSequenceIndex].InvokeSequenceEnded();
            return;
        }
        _currentDialogueIndex++;
        _currentCoroutine = StartCoroutine(_spawner.DisplayTextSequence(tutorialSequences[_currentSequenceIndex].dialogueInSequence[_currentDialogueIndex], tutorialText));
    }

    public void ChangeSequenceIndex()
    {
        _currentSequenceIndex++;
        _currentDialogueIndex = 0;
        EnableAndStartNextSequence();
    }

    private void EnableAndStartNextSequence()
    {
        InDialogue = true;
        tutorialDialogueWindow.SetActive(true);
        interactionBlocker.SetActive(true);
        _currentCoroutine = StartCoroutine(_spawner.DisplayTextSequence(tutorialSequences[_currentSequenceIndex].dialogueInSequence[_currentDialogueIndex], tutorialText));
    }

    private void DisableAllGameCanvasMenus()
    {
        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}

[Serializable]
public class TutorialSequence
{
    public string sequenceName;
    [TextArea]
    public string[] dialogueInSequence;
    public UnityEvent SequenceEnded;

    public void InvokeSequenceEnded()
    {
        SequenceEnded?.Invoke();
    }
}
