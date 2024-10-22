using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectStocker : MonoBehaviour
{
    public bool StockModeEnabled { get; set; }
    
    private MousePositionHandler _positionHandler;

    private ItemSelector _selector;

    private Shelf _currentShelf;

    [SerializeField] private GameObject stockButton; 

    private StockInterface _interface;

    private bool _mouseOverUI;

    private void Start()
    {
        _positionHandler = GetComponent<MousePositionHandler>();
        _selector = FindFirstObjectByType<ItemSelector>();
        _interface = FindFirstObjectByType<StockInterface>();
        
        
        _interface.gameObject.SetActive(false);
    }

    private void Update()
    {
        _mouseOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    public void ChangeModeState(bool newState)
    {
        StockModeEnabled = newState;
    }

    

    public void CheckForShelfToStock()
    {
        if (!StockModeEnabled || _mouseOverUI) return;
        if (_positionHandler.CheckForObjectToInteract() == null) return;
        _currentShelf = _positionHandler.CheckForObjectToInteract().GetComponent<Shelf>();
        _interface?.gameObject.SetActive(true);
        var buttonGrid = _interface?.GetComponentInChildren<GridLayoutGroup>().transform;
        
        foreach (var currentGame in _selector.AllItems)
        {
            /*
            if (_currentShelf.AssignedObject.CompatibleProducts.Any(productType => currentGame.ItemType.ProductType != productType))
            {
                Debug.LogFormat($"Incorrect Game Type.");
                continue;
            }*/

            if (_currentShelf.AssignedObject.CompatibleProducts.All(productType => currentGame.ItemType.ProductType != productType))
            {
                Debug.LogFormat($"Incorrect Game Type.");
                continue;
            }

            if (!currentGame.ItemUnlocked)
            {
                Debug.LogFormat($"Game is locked.");
                continue;
            }
            var tempButton = Instantiate(stockButton, buttonGrid);
            tempButton.GetComponent<Button>().onClick.AddListener(delegate { AssignStock(currentGame.GameID); });
            tempButton.GetComponentInChildren<TMP_Text>().text = currentGame.ItemName;
        }
    }

    public void AssignStock(int gameID)
    {
        _currentShelf.UnstockShelf();
        _currentShelf.StockShelf(gameID, _selector);
        _interface?.gameObject.SetActive(false);
    }
}
