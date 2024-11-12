using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenCharacters;
    [SerializeField] private AudioClip blip;
    private float _waitBetweenSfx = 0.05f;
    private WaitForSeconds _waitTimeBetweenCharacters;
    private WaitForSeconds _waitTimeBetweenSfx;
    private bool running;
    

    public event Action SequenceStarted, SequenceEnded;

    private void Start()
    {
        _waitTimeBetweenCharacters = new WaitForSeconds(timeBetweenCharacters);
        _waitTimeBetweenSfx = new WaitForSeconds(_waitBetweenSfx);
    }

    public IEnumerator DisplayTextSequence(string textToSpawn, TMP_Text textToChange)
    {
        running = true;
        StartCoroutine("AudioSequence");
        SequenceStarted?.Invoke();;
        for (var i = 0; i <= textToSpawn.Length; i++)
        {
            textToChange.text = textToSpawn[..i];
            yield return _waitTimeBetweenCharacters;
        }

        running = false;
        SequenceEnded?.Invoke();
    }

    public IEnumerator AudioSequence()
    {
        while (running)
        {
            SoundManager.PlayClip(blip);
            yield return _waitTimeBetweenSfx;
        }
    }
    
    
}
