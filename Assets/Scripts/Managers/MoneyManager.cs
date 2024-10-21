using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    
    [field:SerializeField]public int Profit { get; set; }
    
    [field:SerializeField]public int Quota { get; set; }
    
    [field:SerializeField]public bool DayFinished { get; private set; }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementProfit(int money)
    {
        Profit += money;
    }
    
    public void DecrementProfit(int money)
    {
        Profit -= money;
    }

    public void DecrementQuota(int money)
    {
        Quota -= money;
        if (Quota <= 0) DayFinished = true;
    }
}
