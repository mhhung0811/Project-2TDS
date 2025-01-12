using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : Singleton<PathRequestManager>
{
	Queue<PathRequest> PathRequestQueue = new Queue<PathRequest>();
	PathRequest CurrentPathRequest;
	PathFinding pathFinding;
	GridManager gridManager;
	bool IsProcessingPath;
	public override void Awake()
	{
		base.Awake();
		pathFinding = GetComponent<PathFinding>();
		gridManager = GetComponent<GridManager>();
	}

	public static void RequestPath(Vector2 pathStart, Vector2 pathEnd, Action<Vector2[], bool> callback)
	{
		PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
		Instance.PathRequestQueue.Enqueue(newRequest);

		Debug.Log("PathRequestQueue.Count: " + Instance.PathRequestQueue.Count);
		Instance.TryProcessNext();
	}

	public void TryProcessNext()
	{
		if(!IsProcessingPath && PathRequestQueue.Count > 0)
		{
			CurrentPathRequest = PathRequestQueue.Dequeue();
			IsProcessingPath = true;
			pathFinding.StartFindPath(CurrentPathRequest.pathStart, CurrentPathRequest.pathEnd);
		}
	}

	public void FinishedProcessingPath(Vector2[] path, bool success)
	{
		CurrentPathRequest.callback(path, success);
		IsProcessingPath = false;
		TryProcessNext();
	}

	struct PathRequest
	{
		public Vector2 pathStart;
		public Vector2 pathEnd;
		public Action<Vector2[], bool> callback;

		public PathRequest(Vector2 _start, Vector2 _end, Action<Vector2[], bool> _callback)
		{
			pathStart = _start;
			pathEnd = _end;
			callback = _callback;
		}
	}
	
	public void UpdatePos(Vector2 pos)
	{
		transform.position = pos;
		gridManager.UpdateGrid();
	}
}