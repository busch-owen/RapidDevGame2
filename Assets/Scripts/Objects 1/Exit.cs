using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private AudioSource _source;
    [SerializeField] private AudioClip Enter;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<NpcStateMachine>()) _source.PlayOneShot(Enter);
    }

    private void Start()
    {
         _source = FindFirstObjectByType<AudioSource>();
    }
}
