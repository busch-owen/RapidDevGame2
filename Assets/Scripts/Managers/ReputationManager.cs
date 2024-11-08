using System;
using UnityEngine;

public class ReputationManager : MonoBehaviour
{
    public static float CurrentReputation { get; private set; }

    public static event Action reputationChanged;

    public static void ChangeReputation(float amt)
    {
        CurrentReputation += amt;
        reputationChanged?.Invoke();
    }
}
