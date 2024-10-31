using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float turboSpeed;
    [SerializeField] private float turboDecreaseRate;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip walk;
    [SerializeField] private Animator animator;
    private Vector2 direction;
    private bool gameOver;
    
    private float turboBar = 100;


    private void Awake()
    {
        GameEvents.Instance.OnItemCollide += updateSpeed;
    }
    

    // Update is called once per frame
    void Update()
    {

        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vKey))
            {
                print(vKey);
            }
        }

        if (gameOver) return;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        var turboActive = Input.GetAxis("ZR");
        float finalSpeed;
        
        var inputMagnitude = Mathf.Clamp01(direction.magnitude);
        direction.Normalize();

        if (turboActive == 1 && turboBar > 0)
        {
            finalSpeed = moveSpeed + turboSpeed;
            turboBar -= turboDecreaseRate * Time.deltaTime;
        }
        else
        {
            finalSpeed = moveSpeed;
        }

        if (turboActive == 0 && turboBar <= 100)
        {
            turboBar += turboDecreaseRate * Time.deltaTime;
        }

        print(turboBar);
        
        transform.Translate(direction * (finalSpeed * inputMagnitude * Time.deltaTime), Space.World);

        if (direction != Vector2.zero)
        {
            animator.SetBool("walking", true);
            var toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("walking", false);
        }
            
    }
    
    private void updateSpeed(int score, float speed)
    {
        moveSpeed += speed;
        rotationSpeed += speed / 30;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Border" || collider.gameObject.tag == "Enemy")
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOver = true;
        GameEvents.Instance.GameOver(gameOver);
        Destroy(gameObject);
    }
}
