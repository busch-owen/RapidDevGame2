using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockInterface : MonoBehaviour
{
    private void OnEnable()
    {
        var buttons = GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            Destroy(button.gameObject);
        }
    }
}
