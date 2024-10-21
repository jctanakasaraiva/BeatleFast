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
    [SerializeField] private int minDistanceToSpawnItem;
    [SerializeField] public List<ItemCharacteistics> items;

    private bool gameOver;
    private GameObject player;

    private Vector3 playerPosition;
    
    private void Start()
    {
        GameEvents.Instance.OnGameOver += GameOver;
        GameEvents.Instance.OnStartGame += StartGame;
        GameEvents.Instance.OnPlayerMove += UpdatePlayerPosition;
    }
    
    private IEnumerator SpawnItem()
    {
        while (!gameOver)
        {
            var positionHorizontal = Random.Range(-horizontal,horizontal + 1);
            var positionVertical = Random.Range(-vertical, vertical + 1);
            if (positionHorizontal <= -35 && positionVertical >= 50) continue;
            var position = new Vector3(positionHorizontal, positionVertical);
            if (!(Vector3.Distance(playerPosition, position) > minDistanceToSpawnItem)) continue;
            var selectedPrefab = GetRandomItemPrefab();
            var gameObject = Instantiate(selectedPrefab, position,Quaternion.identity);
            yield return new WaitForSeconds(secondsToSpawn);
            print(position + " - " + playerPosition + " = " + Vector3.Distance(playerPosition,position));
            Destroy(gameObject, lifeTime);
        }
    }

    private void UpdatePlayerPosition(Vector3 position)
    {
        playerPosition = position;
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

