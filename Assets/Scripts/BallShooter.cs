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
    [SerializeField] private Animator _animator;
    [SerializeField] private bool shootingState;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            _animator.SetBool("Shooting", true);
            var transform1 = transform;
            Instantiate(ballPrefab, transform1.position, transform1.rotation);
                yield return new WaitForSeconds(2);//Random.Range(minTimeToShoot,maxTimeToShoot));
            _animator.SetBool("Shooting", false);
            yield return new WaitForSeconds(2);
        }
    }
}
