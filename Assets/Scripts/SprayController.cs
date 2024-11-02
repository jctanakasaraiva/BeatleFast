using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = System.Numerics.Vector2;

public class SprayController : MonoBehaviour
{
    [SerializeField] private GameObject spray;
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.Play();
        }
    }
}
