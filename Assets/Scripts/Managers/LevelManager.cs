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
        //The current system for this is very wrong mathematically, and so I'll revisit it soon
        
        LevelChanged?.Invoke();
        CurrentLevelProgression += progressionGained;
        if (!(CurrentLevelProgression <= _amtTillNextLevel)) return;
        CurrentLevel++;
        var remainder = progressionGained - CurrentLevelProgression;
        CurrentLevelProgression = remainder;
        if (CurrentLevelProgression <= _amtTillNextLevel)
        {
            IncreaseLevelProgression(remainder);
        }
    }
}
