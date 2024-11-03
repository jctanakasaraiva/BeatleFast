using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float turboSpeed;
    [SerializeField] private float turboDecreaseRate;
    [SerializeField] private AudioSource _walkAudioSource;
    [SerializeField] private AudioSource _biteAudioSource;
    [SerializeField] private Animator animator;
    [SerializeField] private TrailRenderer _trailRenderer;
    
    private Vector2 direction;
    private bool gameOver;
    
    private float turboBarValue = 100;
    private bool turboActive = false;

    private void Awake()
    {
        GameEvents.Instance.OnItemCollide += updateSpeed;
    }
    

    // Update is called once per frame
    void Update()
    {
        #region Gamepad test
        /*
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vKey))
            {
                print(vKey);
            }
        }
        */

        #endregion
        

        if (gameOver) return;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        if (Input.GetAxis("ZR") == 1 || Input.GetKey(KeyCode.Space))
        {
            turboActive = true ;
            _trailRenderer.emitting = true;
        }
        else
        {
            turboActive = false;
            _trailRenderer.emitting = false;
        }
        
        float finalSpeed;
        
        var inputMagnitude = Mathf.Clamp01(direction.magnitude);
        direction.Normalize();

        if (turboActive && turboBarValue > 0)
        {
            _walkAudioSource.pitch = 2;
            finalSpeed = moveSpeed + turboSpeed;
            turboBarValue -= turboDecreaseRate * Time.deltaTime;
            GameEvents.Instance.UpdateTurboSpeed(turboBarValue);
        }
        else
        {
            _walkAudioSource.pitch = 1;
            finalSpeed = moveSpeed;
            GameEvents.Instance.UpdateTurboSpeed(turboBarValue);
        }

        if (!turboActive && turboBarValue <= 100)
        {
            turboBarValue += (turboDecreaseRate/2) * Time.deltaTime;
        }
        
        transform.Translate(direction * (finalSpeed * inputMagnitude * Time.deltaTime), Space.World);

        if (direction != Vector2.zero)
        {
            GameEvents.Instance.PlayerMove(transform.position);
            if (!_walkAudioSource.isPlaying)
            {
                _walkAudioSource.Play();
            }
            animator.SetBool("walking", true);
            var toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            _walkAudioSource.Stop();
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
        if (collider.CompareTag("Border") || collider.CompareTag("Enemy"))
        {
            GameOver();
        }

        if (collider.CompareTag("Item"))
        {
            _biteAudioSource.Play();
        }
    }

    private void GameOver()
    {
        gameOver = true;
        GameEvents.Instance.GameOver(gameOver);
        Destroy(gameObject);
    }
}
