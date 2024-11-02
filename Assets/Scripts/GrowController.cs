using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowController : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToGrow;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float growSpeed;

    private Vector3 objectScale;
    private bool triggerActive;
    
    
    private void Start()
    {
        GameEvents.Instance.OnGameOver += GameOver;
    }

    private void Update()
    {
        if (triggerActive && objectScale.x < maxScale)
        {
            var growth = Time.deltaTime * growSpeed;
            objectScale += new Vector3(growth, growth);
            ObjectToGrow.transform.localScale = objectScale;
        }
        if (!triggerActive && objectScale.x > minScale)
        {
            var growth = Time.deltaTime * growSpeed;
            objectScale -= new Vector3(growth, growth);
            ObjectToGrow.transform.localScale = objectScale;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            triggerActive = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        triggerActive = false;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameOver -= GameOver;
    }

    private void GameOver(bool gameOver)
    {
        Destroy(gameObject);
    }
}
