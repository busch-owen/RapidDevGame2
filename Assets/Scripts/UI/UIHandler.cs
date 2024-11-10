using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text rentText;
    [SerializeField] private Image levelFill;
    [SerializeField] private Image ratingFill;

    [SerializeField] private GameObject buildButton;
    [SerializeField] private Transform buildButtonLayout;
    
    private MoneyManager _moneyManager;
    private LevelManager _levelManager;
    private ObjectPicker _picker;
    private GameEnd _gameEnd;
    private EventManager _eventManager;

    private void Start()
    {
        _moneyManager = FindFirstObjectByType<MoneyManager>();
        _levelManager = FindFirstObjectByType<LevelManager>();
        _eventManager = FindFirstObjectByType<EventManager>();
        _picker = FindFirstObjectByType<ObjectPicker>();
        _gameEnd = FindFirstObjectByType<GameEnd>();
        rentText.text = $"Rent : ${_gameEnd.quota}";
        _moneyManager.MoneyChanged += UpdateMoneyCount;
        _levelManager.LevelChanged += UpdateLevelDisplay;
        _eventManager._NextDay += UpdateDay;
        
        UpdateMoneyCount();
        UpdateLevelDisplay();
        OccupyBuildMenu();
        ReputationManager.reputationChanged += UpdateReputation;
    }

    private void UpdateMoneyCount()
    {
        moneyText.text = $"${_moneyManager.Profit:N}";
    }

    private void UpdateDay()
    {
        dayText.text = $"Day : {_gameEnd.DaysPassed}";
    }

    private void UpdateLevelDisplay()
    {
        levelText.text = $"Level {_levelManager.CurrentLevel}";
        levelFill.fillAmount = _levelManager.CurrentLevelProgression;
    }

    private void OccupyBuildMenu()
    {
        foreach (var obj in _picker.AllObjects)
        {
            var newObj = Instantiate(buildButton, buildButtonLayout);
            newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate{_picker.PickSpecificObject(obj);});
            newObj.GetComponent<PlaceModeButton>().AssignContainer(obj);
        }
    }

    private void UpdateReputation()
    {
        ratingFill.fillAmount = ReputationManager.CurrentReputation;
    }
}
