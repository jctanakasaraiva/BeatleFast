using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStopGame : MonoBehaviour
{
    private void Start()
    {
        GameEvents.Instance.OnStopGame += DoOnStopGame;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnStopGame -= DoOnStopGame;
    }

    private void DoOnStopGame()
    {
        Destroy(gameObject);
        StopAllCoroutines();
    }
}
