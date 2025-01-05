using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public Vector2Variable target;
	public float speed;
	Vector2[] path;
	int targetIndex;
	public bool onDrawGizmos;
	private Rigidbody2D _rb;
	private Coroutine _updatePath;
	private Coroutine _followPath;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		
	}

	public void OnPathFound(Vector2[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			path = newPath;
			targetIndex = 0;
			if(_followPath != null)
			{
				StopCoroutine(_followPath);
			}
			_followPath = StartCoroutine(FollowPath());
		}
	}

	public void StartFindPath()
	{
		 _updatePath = StartCoroutine(UpdatePath());
	}

	public void StopFindPath() {	
		if(_updatePath != null)
			StopCoroutine(_updatePath);

		if(_followPath != null)
			StopCoroutine(_followPath);

		_rb.velocity = Vector2.zero;
	}

	public IEnumerator UpdatePath()
	{
		while (true)
		{
			PathRequestManager.RequestPath(transform.position, target.CurrentValue, OnPathFound);
			yield return new WaitForSeconds(0.2f); 
		}
	}


	public IEnumerator FollowPath()
	{
		if (path == null || path.Length == 0)
		{
			yield break;
		}

		Vector2 currentWaypoint = path[0];

		while (true)
		{
			if ((Vector2)transform.position == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
				{
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			// transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			_rb.velocity = (currentWaypoint - (Vector2)transform.position).normalized * speed;
			yield return null;
		}
	}

	public void OnDrawGizmos()
	{
		if(!onDrawGizmos)
		{
			return;
		}
		if(path != null)
		{
			for (int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube((Vector3)path[i], new Vector3(0.5f,0.5f,0));

				if (i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
		}
	}
}
