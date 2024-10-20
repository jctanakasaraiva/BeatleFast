using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float rotationAngle = 0;
    private bool gameOver;


    private void Awake() => rb = GetComponent<Rigidbody2D>();

    // Update is called once per frame
    void Update()
    {
        movement.y = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (gameOver) return;
        ApplySpeed();
        ApplyRotation();

    }
    
    private void ApplySpeed()
    {
        if (movement.y > 0)
        {
            movement.y += 1;
        }
        else
        {
            movement.y = 1;
        }
        rb.velocity = transform.right * (movement.y * moveSpeed);
    }
    
    private void ApplyRotation()
    {
        rotationAngle = rotationAngle - (movement.x * (rotationSpeed));
        rb.MoveRotation(rotationAngle);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Border")
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOver = true;
        rb.velocity = Vector2.zero;
        GameEvents.Instance.GameOver(gameOver);
        Destroy(gameObject);
    }
}
