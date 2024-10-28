using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreCartInfo : MonoBehaviour
{
    public GameContainer AssignedContainer { get; private set; }

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text countText;

    public event Action ItemRemovedFromCart;
    
    private StockOrderer _orderer;

    private void Start()
    {
        _orderer = FindFirstObjectByType<StockOrderer>();
        ItemRemovedFromCart += _orderer.CalculateCartBalance;
    }

    public void SetAssignedContainer(GameContainer container)
    {
        AssignedContainer ??= new GameContainer
        {
            ItemCount = 0,
            ItemType = container.ItemType,
            ItemName = container.ItemName,
            ItemUnlocked = container.ItemUnlocked
        };
        
        nameText.text = AssignedContainer.ItemType.ItemName;
        AssignedContainer?.ChangeCount(1);
        countText.text = $"X{AssignedContainer?.ItemCount}";
    }

    public void AddToExistingCartItem()
    {
        AssignedContainer?.ChangeCount(1);
        countText.text = $"X{AssignedContainer?.ItemCount}";
        _orderer.CalculateCartBalance();
    }

    public void RemoveAssignedItemFromCart()
    {
        _orderer.itemsInCart.Remove(this);
        ItemRemovedFromCart?.Invoke();
        Destroy(gameObject);
    }
}
