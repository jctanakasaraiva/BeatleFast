using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private int ballSpeed;
    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * ballSpeed);
        Destroy(gameObject, 8);
    }   
}
