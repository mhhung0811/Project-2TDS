using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public Transform target;
	float speed = 2;
	Vector2[] path;
	int targetIndex;

	void Start()
	{
		StartCoroutine(UpdatePath());
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
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator UpdatePath()
	{
		while (true)
		{
			PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
			yield return new WaitForSeconds(0.5f); 
		}
	}


	IEnumerator FollowPath()
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

			transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;
		}
	}

	public void OnDrawGizmos()
	{
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