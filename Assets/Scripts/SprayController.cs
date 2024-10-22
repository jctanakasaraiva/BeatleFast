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
    [SerializeField] private float maxScale;
    [SerializeField] private int spraySpeed;

    private Vector3 sprayScale;
    private bool sprayActive;

    private void Start()
    {
        GameEvents.Instance.OnGameOver += GameOver;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameOver -= GameOver;
    }

    private void Update()
    {
        if (sprayActive && sprayScale.x < maxScale)
        {
            sprayScale.x += Time.deltaTime * spraySpeed; 
            sprayScale.y += Time.deltaTime * spraySpeed; 
            spray.transform.localScale = sprayScale;
            
        }
        if (!sprayActive && sprayScale.x > 0)
        {
            sprayScale.x -= Time.deltaTime * spraySpeed; 
            sprayScale.y -= Time.deltaTime * spraySpeed; 
            spray.transform.localScale = sprayScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        sprayActive = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        sprayActive = false;
    }
    
    private void GameOver(bool gameOver)
    {
        Destroy(gameObject);
    }
}
