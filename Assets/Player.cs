using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float rotationAngle = 0;


    private void Awake() => rb = GetComponent<Rigidbody2D>();

    // Update is called once per frame
    void Update()
    {
        movement.y = 1; // Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        ApplySpeed();
        ApplyRotation();
    }
    
    private void ApplySpeed()
    {
        rb.velocity = transform.right * (movement.y * moveSpeed);
    }
    
    private void ApplyRotation()
    {
        rotationAngle = rotationAngle - (movement.x * rotationSpeed);
        rb.MoveRotation(rotationAngle);
    }
}
