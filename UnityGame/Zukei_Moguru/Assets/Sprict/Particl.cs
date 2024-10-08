using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particl : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip Death;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(Death);
        StartCoroutine(Utilities.DelayMethod(1, () => Destroy(gameObject)));
    }
    void Update()
    {
        
    }
}
