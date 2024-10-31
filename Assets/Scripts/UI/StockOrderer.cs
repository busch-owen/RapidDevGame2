using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StockOrderer : MonoBehaviour
{
    private ItemSelector _itemSelector;
    private MoneyManager _moneyManager;

    [SerializeField] private Transform buttonGrid;
    [SerializeField] public Transform inventoryButtonGrid;
    [SerializeField] private Transform cartLayout;
    [SerializeField] private GameObject purchaseButton;
    [SerializeField] private GameObject stockButton;
    [SerializeField] private GameObject cartItem;
    [SerializeField] private TMP_Text balanceText;

    public List<StoreCartInfo> itemsInCart;
    private float _cartBalance;
    
    private void Start()
    {
        _moneyManager = FindFirstObjectByType<MoneyManager>();
        _itemSelector = FindFirstObjectByType<ItemSelector>();
        LoadItemsToBuy();
    }

    private void LoadItemsToBuy()
    {
        foreach (var item in _itemSelector.AllItems)
        {
            var newItem = item;
            var newButton = Instantiate(purchaseButton, buttonGrid);
            newButton.GetComponentInChildren<Button>().onClick.AddListener(delegate { AddItemsToCart(newItem); });
            newButton.GetComponent<StoreItemButton>().AssignContainer(item);
        }
    }

    private void LoadItemsToStock()
    {
        foreach (var item in _itemSelector.AllItems)
        {
            var newItem = item;
            var newButton = Instantiate(stockButton, inventoryButtonGrid);
            newButton.GetComponent<StockItemButton>().AssignInventoryContainer(newItem);
        }
    }

    private void AddItemsToCart(GameContainer item)
    {
        foreach (var cart in itemsInCart.Where(cart => cart.AssignedContainer.ItemType == item.ItemType))
        {
            cart.AddToExistingCartItem();
            return;
        }
        var newCartItem = Instantiate(cartItem, cartLayout);
        newCartItem.GetComponent<StoreCartInfo>().SetAssignedContainer(item);
        itemsInCart.Add(newCartItem.GetComponent<StoreCartInfo>());
        CalculateCartBalance();
    }

    public void CalculateCartBalance()
    {
        
        _cartBalance = 0;
        if (itemsInCart.Count <= 0)
        {
            balanceText.text = $"${_cartBalance:N}";
            return;
        }
            
        foreach (var item in itemsInCart)
        {
            for (var i = 0; i < item.AssignedContainer.ItemCount; i++)
            {
                _cartBalance += item.AssignedContainer.ItemType.Cost;
            }
            balanceText.text = $"${_cartBalance:N}";
        }
    }

    public void Checkout()
    {
        if (_cartBalance >= _moneyManager.Profit) return;
        foreach (var cartItems in itemsInCart)
        {
            foreach (var inventoryItems in _itemSelector.AllItems.Where(inventoryItems => inventoryItems.ItemType == cartItems.AssignedContainer.ItemType))
            {
                inventoryItems.ChangeCount(cartItems.AssignedContainer.ItemCount);
                var newButton = Instantiate(stockButton, inventoryButtonGrid);
                newButton.GetComponent<StockItemButton>().AssignInventoryContainer(cartItems.AssignedContainer);
            }
        }
        foreach (var item in itemsInCart)
        {
            Destroy(item.gameObject);
        }
        
        _moneyManager.DecrementProfit(_cartBalance);
        itemsInCart.Clear();
        CalculateCartBalance();
    }
}
