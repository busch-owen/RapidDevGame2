using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npc;

    private NPCAI _npcai;

    [SerializeField] private Transform entrance;


    public void SpawnNpc()
    {
        _npcai = npc.GetComponent<NPCAI>();
        var spawnedNpc = Instantiate(npc, entrance.transform.position, entrance.transform.rotation);
    }
}
