using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTask : MonoBehaviour
{
    public List<SwipePoint> _swipePoints = new();
    public float CountDownMax;
    private int _currentSwipePointIndex = 0;
    private float _countDown = 0;
    public GameObject Green;
    public GameObject Red;
    public MoneyManager Money;
    private NpcStateMachine Npc;
    private int _itemIndex = 0;
    [SerializeField] private List<ItemTypeSo> items = new();
    [SerializeField] private GameObject _canvas;

    // Update is called once per frame
    void Update()
    {
        _countDown -= Time.deltaTime;
        if (_currentSwipePointIndex != 0 && _countDown <= 0)
        {
            _currentSwipePointIndex = 0;
            StartCoroutine(FinishTask(false));
            Debug.Log("aw dang it");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<NpcStateMachine>())
        {
            var npc = other.GetComponentInParent<NpcStateMachine>();
            Npc = npc;
            _canvas.SetActive(true);
            items = Npc.ItemsCollected;

        }
    }

    private void Start()
    {
        Red.SetActive(false);
        Green.SetActive(false);
        Money = FindFirstObjectByType<MoneyManager>();
        _canvas.SetActive(false);
    }

    private IEnumerator FinishTask(bool Successful)
    {
        if (Successful)
        {
            Green.SetActive(true);
        }
        else
        {
            Red.SetActive(true);
        }

        yield return new WaitForSeconds(1.0f);
        Red.SetActive(false);
        Green.SetActive(false);
    }
    public void SwipePointTrigger(SwipePoint swipePoint)
    {
        if (swipePoint == _swipePoints[_currentSwipePointIndex])
        {
            _currentSwipePointIndex++;
            _countDown = CountDownMax;
        }

        if (_currentSwipePointIndex >= _swipePoints.Count)
        {
            _currentSwipePointIndex = 0;
            StartCoroutine(FinishTask(true));
            swipePoint.itemToSwipe.Item = Npc.ItemsCollected[_itemIndex];
            Money.IncrementProfit(swipePoint.itemToSwipe.Item.Cost);
            _itemIndex++;
            Debug.Log("Yipee");
            if (_itemIndex >= items.Count)
            {
                Npc.ChangeState(NpcStateName.Exit);
            }
        }
    }
}
