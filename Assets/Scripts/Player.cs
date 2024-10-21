using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip walk;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float rotationAngle = 0;
    private bool gameOver;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameEvents.Instance.OnItemCollide += updateSpeed;
    }


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

    private void updateSpeed(int score, float speed)
    {
        moveSpeed += speed;
        rotationSpeed += speed / 30;
    }
    
    private void ApplySpeed()
    {
        if (movement.y > 0)
        {
            movement.y += 1;
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = walk;
                _audioSource.pitch = 2.5f;
                _audioSource.Play();
            }
        }
        else
        {
            movement.y = 1;
            
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = walk;
                _audioSource.pitch = 2;
                _audioSource.Play();
            }
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
