using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
    [SerializeField] public List<ItemTypeSo> items = new();
    [SerializeField] private GameObject _canvas;
    [SerializeField] private ItemToSwipe _toSwipe;
    private bool _alreadySpawned;
    [SerializeField] private AudioClip _goodClip;
    [SerializeField] private AudioClip _badClip;
    [SerializeField] private AudioSource _source;
    public bool Correct;
    private EventManager _eventManager;
    public bool CheckingOut;
    private ItemToSwipe _itemToSwipe;
    public bool Done;

    public Transform ItemSpawn;
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

    private void AssignItems(List<ItemTypeSo> Items)
    {
        items = Items;
        Debug.Log("assigned");
        Invoke("EnableCheckout",2.0f);
    }

    private void AssignNpc(NpcStateMachine npc)
    {
        Npc = npc;
    }

    private void EnableCheckout()
    {
        _canvas.SetActive(true);
        Done = false;

        foreach (var item in items)
        {
            var Scan = _toSwipe;
            if (!_alreadySpawned)
            {
                _toSwipe.Item = item;
                
                Scan = Instantiate(_toSwipe, ItemSpawn);
                Scan.transform.SetParent(ItemSpawn);
                _alreadySpawned = true;
            }
            else
            {
                Scan.Item = item;
            }
        }

    }

    private void Start()
    {
        Red.SetActive(false);
        Green.SetActive(false);
        Money = FindFirstObjectByType<MoneyManager>();
        _canvas.SetActive(false);
        _source = FindFirstObjectByType<AudioSource>();
        _eventManager = FindFirstObjectByType<EventManager>();
        
        _eventManager.Arrived += AssignItems;
        _eventManager.Npc += AssignNpc;

    }

    private IEnumerator FinishTask(bool Successful)
    {
        if (Successful)
        {
            Correct = true;
            _source.PlayOneShot(_goodClip);
        }
        else
        {
            Red.SetActive(true);
            Correct = false;
            _source.PlayOneShot(_badClip);
        }

        yield return new WaitForSeconds(1.0f);
        Red.SetActive(false);
        Green.SetActive(false);
        Correct = false;
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
            Money.IncrementProfit(_toSwipe.Item.Cost);
            _itemIndex++;
            Green.SetActive(true);
            Debug.Log("Yipee");
            if (_itemIndex >= items.Count)
            {
                Invoke("Disable", 1.0f);
            }
        }
    }

    private void Disable()
    {
        Done = true;
        Red.SetActive(false);
        Green.SetActive(false);
        Npc.ChangeState(NpcStateName.Exit);
        _canvas.SetActive(false);
    }
}
