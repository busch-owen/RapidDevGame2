using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npc;

    private WaitForSeconds _timeToNextWave;

    [SerializeField] private float time;

    [SerializeField] private Transform entrance;
    
    [field: SerializeField] public int Entrances { get; private set; }
    
    [field: SerializeField] public List<GameObject> Cars{ get; private set; } = new();
    
    [field: SerializeField] public List<int> Waves{ get; private set; } = new();
    
    [field: SerializeField] public List<int> AmountOfNpcs{ get; private set; } = new();
    
    [field: SerializeField] public int MaxTimeBetweenNpcs { get;  set; }
    
    [field: SerializeField] public int MinTimeBetweenNpcs { get;  set; }
    
    private int _npcsToSpawn;

    private bool _running;

    private EventManager _eventManager;

    private GameEnd _gameEnd;

    private int TotalWaves = 0;


    public void SpawnNpc()
    {
        Entrances = Random.Range(0, Cars.Count);// choose cars to spawn npcs at

        entrance = Cars[Entrances].transform;
        var spawnedNpc = Instantiate(npc, entrance.transform.position, quaternion.identity);// instantiate the npcs at said cars
    }

    public void StartDay()
    {
        StartCoroutine("RandomizeNpcs");// randomize the number of npcs which when finished will spawn the first wave
    }

    public void EndDay()
    {
        StopCoroutine("NpcWaveSpawn");
        
    }

    public void NextDay()
    {
        time--;
        MaxTimeBetweenNpcs-= 2;
    }

    public IEnumerator NpcWaveSpawn()
    {
        time = Random.Range(MinTimeBetweenNpcs, MaxTimeBetweenNpcs);
        _timeToNextWave = new WaitForSeconds(time);
        while (_gameEnd.DaysPassed < _gameEnd.DaysTillRent)// for each wave of npcs spawn the amount of npcs specified then wait until it is time for the next wave to do it again
        {
            foreach (int amtofNpcs in AmountOfNpcs)
            {
                SpawnNpc();
            }
            Invoke("Randomize", time -1);// makes npcs get randomized before the next wave
            TotalWaves++;
            yield return _timeToNextWave;
        }

        if (TotalWaves >= Waves.Count)
        {
        }
        
    }

    private void Randomize()// at the end of every wave re randomize the number of npcs
    {
        StartCoroutine("RandomizeNpcs");
    }

    private IEnumerator RandomizeNpcs()
    {
        for (int i = 0; AmountOfNpcs.Count <= _npcsToSpawn; i++)// add to a list of npcs so that the foreach loop has data to pull from
        {
            AmountOfNpcs.Add(i);
        }

        if (!_running)// if the first time starting auto run the next wave
        {
            _running = true;
            yield return StartCoroutine("NpcWaveSpawn");
        }
        else// otherwise do nothing
        {
            yield return null;
        }
        
    }

    private void Start()
    {
        _timeToNextWave = new WaitForSeconds(time);
        _eventManager = FindFirstObjectByType<EventManager>();
        _gameEnd = FindFirstObjectByType<GameEnd>();
        _eventManager._Ended += NextDay;
    }
}
