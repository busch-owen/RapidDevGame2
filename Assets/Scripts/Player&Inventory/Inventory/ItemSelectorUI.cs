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
    private void PopulateItemButton(ItemSO itemData)
    {
        
        var newButton = Instantiate(itemButtonPrefab, itemButtonPanel);
        newButton.SetButton(itemData);

        // set event listener to button for item
        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener( () => {
              itemSelector.allItems.Remove(itemData); itemSelector.reLoadItems();
        } );//lambda function

    }
    private void PopulateItemSelection(ItemSO itemData)
    {
        txtName.text = itemData.itemName;
        txtDescription.text = itemData.itemDescription;
        itemIcon.sprite = itemData.bigIcon;
    }

}
