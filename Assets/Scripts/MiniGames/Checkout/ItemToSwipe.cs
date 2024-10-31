using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemToSwipe : MonoBehaviour, IDragHandler
{
    private Canvas _canvas;

    public ItemTypeSo Item;

    public Image Image;

    private SwipeTask _swipeTask;
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, eventData.position,
            _canvas.worldCamera, out pos);

        transform.position = _canvas.transform.TransformPoint(pos);
    }

    private void FixedUpdate()
    {
        if (_swipeTask.Done)
        {
            Invoke("Destory",1.0f);
        }
    }

    private void Destory()
    {
        Destroy(this.gameObject);
    }

    private void Start()
    {
        Image = GetComponent<Image>();
        _canvas = GetComponentInParent<Canvas>();
        Image.sprite = Item.SmallIcon;
        _swipeTask = FindFirstObjectByType<SwipeTask>();
    }
}
