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
    
    [field: SerializeField] public int MaxNpcs { get;  set; }
    
    [field: SerializeField] public int MinNpcs { get;  set; }
    
    private int _npcsToSpawn;

    private bool _running;


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

    public IEnumerator NpcWaveSpawn()
    {
        foreach (int amtofWaves in Waves)// for each wave of npcs spawn the amount of npcs specified then wait until it is time for the next wave to do it again
        {
            
            foreach (int amtofNpcs in AmountOfNpcs)
            {
                SpawnNpc();
            }
            Invoke("Randomize", time -1);// makes npcs get randomized before the next wave
            yield return _timeToNextWave;
        }
    }

    private void Randomize()// at the end of every wave re randomize the number of npcs
    {
        StartCoroutine("RandomizeNpcs");
    }

    private IEnumerator RandomizeNpcs()
    {
        _npcsToSpawn = Random.Range(MinNpcs, MaxNpcs);// pick the amount of npcs to add
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
    }
}
