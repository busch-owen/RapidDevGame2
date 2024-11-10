using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{

    private EventManager _eventManager;

    public GameObject end;

    public int DaysTillRent = 7;

    public int quota = 1500;

    private MoneyManager _moneyManager;

    [field: SerializeField] public int DaysPassed { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        _moneyManager = FindFirstObjectByType<MoneyManager>();
        _eventManager = FindFirstObjectByType<EventManager>();
        _eventManager._Ended += EndDay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndDay()
    {
        DaysPassed++;
        _eventManager.InvokeNextDay();
        if (DaysPassed >= DaysTillRent)
        {
            if (_moneyManager.Profit >= quota)
            {
                SceneManager.LoadScene("GoodEnd");
            }
            else
            {
                SceneManager.LoadScene("End");
            }
        }
        
    }
}
