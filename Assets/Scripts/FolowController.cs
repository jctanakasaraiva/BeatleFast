using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FolowController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float breakSpeed;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private GameObject objectToFolow;
    private bool folowObject;
    
    void Start()
    {
        objectToFolow = GameObject.FindWithTag("Player");
        folowObject = false;
    }

    private void FixedUpdate()
    {
        if (folowObject)
        {
            _rigidbody2D.AddForce((objectToFolow.transform.position - transform.position) * speed);
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            folowObject = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            folowObject = false;
        }
        
    }
}
