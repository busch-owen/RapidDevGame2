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
    [SerializeField] private List<ItemTypeSo> items = new();
    [SerializeField] private GameObject _canvas;
    [SerializeField] private ItemToSwipe _toSwipe;
    private bool _alreadySpawned;
    [SerializeField] private AudioClip _goodClip;
    [SerializeField] private AudioClip _badClip;
    [SerializeField] private AudioSource _source;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<NpcStateMachine>())
        {
            var npc = other.GetComponentInParent<NpcStateMachine>();
            Npc = npc;
            _canvas.SetActive(true);
            items = Npc.ItemsCollected;
            if(_alreadySpawned)return;
            foreach (var item in items)
            {
                var Spawned = Instantiate(_toSwipe, transform.position, quaternion.identity);
                Spawned.transform.SetParent(ItemSpawn, false);
                Spawned.Item = item;
            }

            _alreadySpawned = true;

        }
    }

    private void Start()
    {
        Red.SetActive(false);
        Green.SetActive(false);
        Money = FindFirstObjectByType<MoneyManager>();
        _canvas.SetActive(false);
        _source = FindFirstObjectByType<AudioSource>();
    }

    private IEnumerator FinishTask(bool Successful)
    {
        if (Successful)
        {
            Green.SetActive(true);
            _source.PlayOneShot(_goodClip);
        }
        else
        {
            Red.SetActive(true);
            _source.PlayOneShot(_badClip);
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
                Invoke("Disable", 1.0f);
            }
        }
    }

    private void Disable()
    {
        Npc.ChangeState(NpcStateName.Exit);
        _canvas.SetActive(false);
    }
}
