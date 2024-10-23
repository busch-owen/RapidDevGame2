using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextIndex : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update

    [SerializeField] private string fullString;
    private string _currentString;

    [SerializeField] private TMP_Text textMeshPro;

    [SerializeField] private float timeBetweenChar;

    private WaitForSeconds _waitBetweenCharacters;

    public float TimeTillTextDisapear = 0.5f;

    private bool _coroutineRunning;

    [field: SerializeField] public List<Image> Emotes { get; set; } = new();
    
    [field: SerializeField] public List<Image> CurentEmotes { get; set; } = new();
    
    [field: SerializeField] public GameObject ImageContainer { get; set; }

    private Image _currentImage;

    private NpcStateMachine _stateMachine;
    
    
    void Start()
    {
        _waitBetweenCharacters = new WaitForSeconds(timeBetweenChar);
        _stateMachine = GetComponentInParent<NpcStateMachine>();
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
            Destroy(image.gameObject, _stateMachine.TimeForFirstWander);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        _stateMachine.OpenWindow();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(2,2,2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1,1,1);
    }
}
