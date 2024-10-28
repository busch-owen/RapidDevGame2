using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int CurrentLevel { get; private set; }
    
    //Level progression is a value from 0-1, so make sure your XP amounts are less than 1.
    public float CurrentLevelProgression { get; private set; }

    private float _amtTillNextLevel;
    [SerializeField] private float levelScaleAmount;

    public event Action LevelChanged;

    //Call this function where ever you see fit for XP progression
    public void IncreaseLevelProgression(float progressionGained)
    {
        LevelChanged?.Invoke();
        CurrentLevelProgression += progressionGained;
        if (!(CurrentLevelProgression >= 1)) return;
        float valueToAddBackToProgression = CurrentLevel -= 1;

        CurrentLevelProgression = valueToAddBackToProgression;
        CurrentLevel++;
    }
}
