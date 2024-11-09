using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StockVisual : MonoBehaviour
{
    [SerializeField] int shelfItems;
    [SerializeField] Sprite oneStockSprite;
    [SerializeField] Sprite lowStockSprite;
    [SerializeField] Sprite lowMedStockSprite;
    [SerializeField] Sprite medStockSprite;
    [SerializeField] Sprite highStockSprite;
    [SerializeField] Sprite fullStockSprite;
    [SerializeField] SpriteRenderer sR;
    [SerializeField] Color gameCategoryColor;
    private Shelf _shelf;
    void Start()
    {
        _shelf = GetComponentInParent<Shelf>();
        shelfItems = 0;
        sR.color = gameCategoryColor;
    }
    void Update()
    {

        shelfItems = _shelf.StockPerRow.Sum();
        if (shelfItems>=18)
        {
            sR.sprite= fullStockSprite;
            
        }
        else if (shelfItems>=12)
        {
            sR.sprite= highStockSprite;
            
        }
        else if (shelfItems>=9)
        {
            sR.sprite= medStockSprite;

        }
        else if (shelfItems>=6)
        {
            sR.sprite= lowMedStockSprite;
        }
        else if (shelfItems>=3)
        {
            sR.sprite= lowStockSprite;
        }
        else if (shelfItems>=1)
        {
            sR.sprite= oneStockSprite;
            
        }
        else if (shelfItems==0)
        {
            sR.sprite= null;
        }
    }
}



