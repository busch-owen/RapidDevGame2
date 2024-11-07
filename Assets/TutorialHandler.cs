using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject startButton, toolPanel, statsPanel, computerPanel;
    [SerializeField] private GameObject buildButton, moveButton, navigateButton, demolishButton;

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
        
        //Sequence Specific Event Assigning
        tutorialSequences[0].SequenceEnded += delegate { toolPanel.SetActive(true); navigateButton.SetActive(true); };
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
        startButton.SetActive(false);
        toolPanel.SetActive(false);
        statsPanel.SetActive(false);
        computerPanel.SetActive(false);
        buildButton.SetActive(false);
        moveButton.SetActive(false);
        navigateButton.SetActive(false);
        demolishButton.SetActive(false);
    }
}

[Serializable]
public class TutorialSequence
{
    public string sequenceName;
    [TextArea]
    public string[] dialogueInSequence;
    public event Action SequenceEnded;

    public void InvokeSequenceEnded()
    {
        SequenceEnded?.Invoke();
    }
}
