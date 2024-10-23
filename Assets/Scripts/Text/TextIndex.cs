using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextIndex : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string fullString;
    private string _currentString;

    [SerializeField] private TMP_Text textMeshPro;

    [SerializeField] private float timeBetweenChar;

    private WaitForSeconds _waitBetweenCharacters;

    public float TimeTillTextDisapear = 0.5f;

    public event Action<Image> ISpawn; 

    public event Action TextFinished;

    private bool _coroutineRunning;

    [field: SerializeField] public List<Image> Emotes { get; set; } = new();
    
    [field: SerializeField] public List<Image> CurentEmotes { get; set; } = new();
    
    [field: SerializeField] public GameObject ImageContainer { get; set; }

    private Image _currentImage;
    
    
    void Start()
    {
        _waitBetweenCharacters = new WaitForSeconds(timeBetweenChar);
    }

    public IEnumerator ImageVisible()
    {
        ImageContainer.SetActive(true);
        foreach (var images in CurentEmotes)
        {
            var image = Instantiate(images, Vector3.zero, Quaternion.identity);
            image.transform.SetParent(ImageContainer.transform, false);
            image.rectTransform.anchoredPosition = Vector3.zero;
            CurentEmotes.Remove(image);
            Destroy(image.gameObject, 2.0f);
            yield return _waitBetweenCharacters;
        }
    }
    
    private void DisableText()
    {
        textMeshPro.enabled = false;
    }

    public void EnableText()
    {
        textMeshPro.enabled = true;
    }

    public void AddEmotes(Image npcImages)
    {
        CurentEmotes.Add(npcImages);
    }
    
    public void RemoveEmotes(Image npcImages)
    {
        CurentEmotes.Remove(npcImages);
    }
}
