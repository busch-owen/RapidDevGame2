using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEnding : MonoBehaviour
{
    [SerializeField] private AudioClip goodClip;
    void Start()
    {
        SoundManager.PlayClip(goodClip);
    }
}
