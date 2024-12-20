using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemButton : MonoBehaviour
{
    private GameContainer _assignedContainer;
    
    [SerializeField] private Image buttonImage;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemCost;

    private TutorialHandler _tutorial;

    private void Start()
    {
        _tutorial = FindFirstObjectByType<TutorialHandler>();
    }

    public void AssignContainer(GameContainer container)
    {
        _assignedContainer = container;
        buttonImage.sprite = _assignedContainer.ItemType.BigIcon;
        itemName.text = _assignedContainer.ItemType.ItemName;
        itemCost.text = $"${_assignedContainer.ItemType.Cost:N}";
    }

    public void RemoveTutorialProgression()
    {
        if (!_tutorial) return;
        GetComponentInChildren<Button>().onClick.RemoveListener(_tutorial.ChangeSequenceIndex);
        _tutorial = null;
    }
}
