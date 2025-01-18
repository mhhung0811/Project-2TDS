using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RenderMinimap : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void OnTriggerEnter2D(Collider2D collision)
	{
		TilemapRenderer tilemapRenderer = collision.GetComponent<TilemapRenderer>();
		if (tilemapRenderer != null)
		{
			tilemapRenderer.enabled = true;
		}
		
		SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
		{
			spriteRenderer.enabled = true;
		}
	}
}
