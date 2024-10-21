using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npc;

    private NPCAI _npcai;

    private WaitForSeconds _timeToNextWave;

    [SerializeField] private float time;

    [SerializeField] private Transform entrance;
    
    [field: SerializeField] public int Entrances { get; private set; }
    
    [field: SerializeField] public List<GameObject> Cars{ get; private set; } = new();
    
    [field: SerializeField] public List<int> Waves{ get; private set; } = new();
    
    [field: SerializeField] public List<int> AmountOfNpcs{ get; private set; } = new();


    public void SpawnNpc()
    {
        _npcai = npc.GetComponent<NPCAI>();
        
        Entrances = Random.Range(0, Cars.Count);

        entrance = Cars[Entrances].transform;
        var spawnedNpc = Instantiate(npc, entrance.transform.position, quaternion.identity);
    }

    public void StartDay()
    {
        StartCoroutine("NpcWaveSpawn");
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
            yield return _timeToNextWave;
        }
        
    }

    private void Start()
    {
        _timeToNextWave = new WaitForSeconds(time);
    }
}
