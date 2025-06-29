using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLineLaze : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LayerMask layerWall;
	public float timeToDestroy;
	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}
	void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void Initialize(Vector2 direction, float timeDestroy)
    {
		timeToDestroy = timeDestroy;
		lineRenderer.SetPosition(0, transform.position);

		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 100, layerWall);
		if (hit.collider != null)
		{
			lineRenderer.SetPosition(1, hit.point);
			Debug.Log("Hit: " + hit.collider.name + "pos: " + hit.point);
		}
		else
		{
			lineRenderer.SetPosition(1, transform.position + (Vector3)direction.normalized * 100f);
		}
		StartCoroutine(DesTroyLine());
	}

	public IEnumerator DesTroyLine()
	{
		yield return new WaitForSeconds(timeToDestroy);
		Destroy(gameObject);
	}
}
