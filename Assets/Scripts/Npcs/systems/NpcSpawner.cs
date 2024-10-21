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
        Entrances = Random.Range(0, Cars.Count);

        entrance = Cars[Entrances].transform;
        var spawnedNpc = Instantiate(npc, entrance.transform.position, quaternion.identity);
    }

    public void StartDay()
    {
        StartCoroutine("RandomizeNpcs");
    }

    public void EndDay()
    {
        StopCoroutine("NpcWaveSpawn");
        
    }

    public IEnumerator NpcWaveSpawn()
    {
        foreach (int amtofWaves in Waves)
        {
            
            foreach (int amtofNpcs in AmountOfNpcs)
            {
                SpawnNpc();
            }
            Invoke("Randomize", time -1);
            yield return _timeToNextWave;
        }
    }

    private void Randomize()
    {
        StartCoroutine("RandomizeNpcs");
    }

    private IEnumerator RandomizeNpcs()
    {
        _npcsToSpawn = Random.Range(MinNpcs, MaxNpcs);
        for (int i = 0; AmountOfNpcs.Count <= _npcsToSpawn; i++)
        {
            AmountOfNpcs.Add(i);
        }

        if (!_running)
        {
            _running = true;
            yield return StartCoroutine("NpcWaveSpawn");
        }
        else
        {
            yield return null;
        }
        
    }

    private void Start()
    {
        _timeToNextWave = new WaitForSeconds(time);
    }
}
