using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockInterface : MonoBehaviour
{
    [SerializeField] private Transform layout;
    private void OnEnable()
    {
        var buttons = layout.GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            Destroy(button.gameObject);
        }
    }
}
