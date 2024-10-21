using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSelectorUI : MonoBehaviour
{
    [SerializeField] ItemSelector itemSelector;
    [SerializeField] ItemButton itemButtonPrefab;
    [SerializeField] public Transform itemButtonPanel;
    [SerializeField] public GameObject itemButtonPanelGameObject;
    // Weapon Stats
    [Header("Item Info Box")]
    [SerializeField] TMP_Text txtName;
    [SerializeField] TMP_Text txtDescription;
    [SerializeField] TMP_Text txtCategory;

    [SerializeField] Image itemIcon;

    private void OnEnable()
    {
        itemSelector.OnItemLoad += PopulateItemButton;
        itemSelector.OnItemSelected += PopulateItemSelection;
    }
    private void OnDisable()
    {
        itemSelector.OnItemLoad -= PopulateItemButton;
        itemSelector.OnItemSelected -= PopulateItemSelection;
    }
    private void PopulateItemButton(InventoryContainter itemData)
    {
        var newButton = Instantiate(itemButtonPrefab, itemButtonPanel);
        newButton.SetButton(itemData);

        // set event listener to button for item
        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener( () => {
            itemSelector.SelectItem(itemData);
        } );//lambda function

    }
    private void PopulateItemSelection(InventoryContainter itemData)
    {
        txtName.text = itemData.ItemType.ItemName;
        txtDescription.text = itemData.ItemType.ItemDescription;
        txtCategory.text = itemData.ItemType.ItemCategory;
        itemIcon.sprite = itemData.ItemType.BigIcon;
    }
}
