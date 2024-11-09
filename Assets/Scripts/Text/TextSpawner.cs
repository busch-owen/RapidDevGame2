using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenCharacters;
    private WaitForSeconds _waitTimeBetweenCharacters;

    public event Action SequenceStarted, SequenceEnded;

    private void Start()
    {
        _waitTimeBetweenCharacters = new WaitForSeconds(timeBetweenCharacters);
    }

    public IEnumerator DisplayTextSequence(string textToSpawn, TMP_Text textToChange)
    {
        SequenceStarted?.Invoke();;
        for (var i = 0; i <= textToSpawn.Length; i++)
        {
            textToChange.text = textToSpawn[..i];
            yield return _waitTimeBetweenCharacters;
        }
        SequenceEnded?.Invoke();
    }
}
