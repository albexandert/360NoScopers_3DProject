using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    SHOOT,
    PICKUP,
    DROP,
    MONSTERGROWL1,
    MONSTERGROWL2,
    BUTTONPRESS
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], 1);
    }
}
