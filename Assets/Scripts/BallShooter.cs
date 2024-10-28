using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float minTimeToShoot;
    [SerializeField] private float maxTimeToShoot;
    [SerializeField] private Animator animator;
    private static readonly int Shooting = Animator.StringToHash("Shooting");

    private void Start()
    {
        GameEvents.Instance.OnGameOver += GameOver;
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            animator.SetBool(Shooting, true);
            var transform1 = transform;
            Instantiate(ballPrefab, transform1.position, transform1.rotation);
            yield return new WaitForSeconds(Random.Range(minTimeToShoot, maxTimeToShoot));
            animator.SetBool(Shooting, false);
            yield return new WaitForSeconds(2);
        }
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
