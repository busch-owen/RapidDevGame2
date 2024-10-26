using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Image levelFill;
    [SerializeField] private Image ratingFill;
    
    private MoneyManager _moneyManager;
    private LevelManager _levelManager;

    private void Start()
    {
        _moneyManager = FindFirstObjectByType<MoneyManager>();
        _levelManager = FindFirstObjectByType<LevelManager>();
        _moneyManager.MoneyChanged += UpdateMoneyCount;
        _levelManager.LevelChanged += UpdateLevelDisplay;
        UpdateMoneyCount();
        UpdateLevelDisplay();
    }

    private void UpdateMoneyCount()
    {
        moneyText.text = $"${_moneyManager.Profit}";
    }

    private void UpdateLevelDisplay()
    {
        levelText.text = $"Level {_levelManager.CurrentLevel}";
        levelFill.fillAmount = _levelManager.CurrentLevelProgression;
    }
}
