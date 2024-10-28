using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryObject : MonoBehaviour
{
    private GameContainer _assignedContainer;
    
    [SerializeField] private Image buttonImage;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemCount;

    private int _lastCount;

    private void Update()
    {
        if (_assignedContainer.ItemCount == _lastCount) return;
        
        itemCount.text = $"X{_assignedContainer.ItemCount}";
        _lastCount = _assignedContainer.ItemCount;
    }

    public void AssignContainer(GameContainer container)
    {
        _assignedContainer = container;
        buttonImage.sprite = _assignedContainer.ItemType.BigIcon;
        itemName.text = _assignedContainer.ItemType.ItemName;
        itemCount.text = $"X{_assignedContainer.ItemCount}";
        _lastCount = _assignedContainer.ItemCount;
    }
}
