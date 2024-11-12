using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public static void PlayClip(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }
}
