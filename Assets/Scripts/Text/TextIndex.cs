using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextIndex : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string fullString;
    private string _currentString;

    [SerializeField] private TMP_Text textMeshPro;

    [SerializeField] private float timeBetweenChar;

    private WaitForSeconds _waitBetweenCharacters;
    
    
    
    void Start()
    {
        _waitBetweenCharacters = new WaitForSeconds(timeBetweenChar);
        StartCoroutine(TextVisible(fullString));
    }


    public IEnumerator TextVisible(string message)
    {
        for (var i = 0; i <= message.Length; i++)
        {
            _currentString = message.Substring(0, i);
            textMeshPro.text = _currentString;
            yield return _waitBetweenCharacters;
        }
    }
}
