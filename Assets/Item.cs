using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    private Collider2D itemCollider2D;
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        itemCollider2D = GetComponent<Collider2D>();

        GameEvents.Instance.OnDoorAreaEnter += HideItem;
        GameEvents.Instance.OnDoorAreaExit += ShowItem;
    }

    private void HideItem()
    {
        SpriteRenderer.enabled = false;
        itemCollider2D.enabled = false;
    }

    private void ShowItem()
    {
        SpriteRenderer.enabled = true;
        itemCollider2D.enabled = true;
    }
}
