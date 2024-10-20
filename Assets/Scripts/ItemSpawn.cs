using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private float secondsToSpawn;
    [SerializeField] private float lifeTime;
    [SerializeField] private int horizontal;
    [SerializeField] private int vertical;
    
    [SerializeField] public List<ItemCharacteistics> items;
    private bool gameOver;
    private void Start()
    {
        GameEvents.Instance.OnGameOver += GameOver;
        GameEvents.Instance.OnStartGame += StartGame;
        //StartCoroutine(SpawnItem());
    }

    private IEnumerator SpawnItem()
    {
        while (!gameOver)
        {
            var positionHorizontal = Random.Range(-horizontal,horizontal + 1);
            var positionVertical = Random.Range(-vertical, vertical + 1);
            if (positionHorizontal <= -35 && positionVertical >= 50) continue;
            var position = new Vector3(positionHorizontal, positionVertical);
            GameObject selectedPrefab = GetRandomItemPrefab();
            var gameObject = Instantiate(selectedPrefab, position,Quaternion.identity);
            yield return new WaitForSeconds(secondsToSpawn);
            Destroy(gameObject, lifeTime);

        }
    }


    private GameObject GetRandomItemPrefab()
    {
        
        var totalChance = 0f;
        foreach (ItemCharacteistics item in items)
        {
            totalChance += item.chance;
        }

        float randomValue = Random.Range(0, totalChance);
        float cumulativeChance = 0f;

        foreach (ItemCharacteistics item in items)
        {
            cumulativeChance += item.chance;
            if (randomValue < cumulativeChance)
            {
                return item.prefab;
            }
        }
        return null;
    }

    private void GameOver(bool gameOverStatus)
    {
        gameOver = gameOverStatus;
    }

    private void StartGame()
    {
        gameOver = false;
        StartCoroutine(SpawnItem());
    }
}

