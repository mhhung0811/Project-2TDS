using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PathFinding : MonoBehaviour
{
	public Transform seeker, target;
	public Vector2 startPointGrid;
	GridManager gridManager;
	private void Awake()
	{
		gridManager = GetComponent<GridManager>();
	}

	private void Update()
	{
		//FindPath(seeker.position, target.position);
	}
	public void OnFindPath(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			StartCoroutine(FindPath(seeker.position, target.position));
		}
	}

	public void StartFindPath(Vector2 startPosition, Vector2 targetPosition)
	{
		StartCoroutine(FindPath(startPosition, targetPosition));
	}
	IEnumerator FindPath(Vector2 startPosition, Vector2 targetPosition)
	{
		Stopwatch sw = new Stopwatch();
		sw.Start();
		Vector2[] waypoints = new Vector2[0];
		bool pathSuccess = false;

		Node startNode = gridManager.GetNodeFromWorldPoint(startPosition);
		Node targetNode = gridManager.GetNodeFromWorldPoint(targetPosition);

		if(startNode.Walkable && targetNode.Walkable)
		{
			Heap<Node> openSet = new Heap<Node>(gridManager.GetMaxSize()); // List of node need to check
			HashSet<Node> closedSet = new HashSet<Node>(); // List of node have checked
			openSet.Add(startNode);

			while(openSet.Count > 0)
			{
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);

				if (currentNode == targetNode) // If current node is target node
				{
					sw.Stop();
					print("Path found: " + sw.ElapsedMilliseconds + " ms");
					pathSuccess = true;
					RetracePath(startNode,targetNode);
					break;
				}

				foreach (Node neighbor in gridManager.GetNeighbors(currentNode))
				{
					if (!neighbor.Walkable || closedSet.Contains(neighbor))
					{
						continue;
					}

					int newCostToNeighbor = currentNode.GCost + gridManager.GetDistance(currentNode, neighbor);
					if (newCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
					{
						neighbor.GCost = newCostToNeighbor;
						neighbor.HCost = gridManager.GetDistance(neighbor, targetNode);
						neighbor.Parent = currentNode;

						if (!openSet.Contains(neighbor))
						{
							openSet.Add(neighbor);
						}
						else
						{
							openSet.UpdateItem(neighbor);
						}
					}
				}
			}
		}
		yield return null;
		if(pathSuccess)
		{
			waypoints = RetracePath(startNode, targetNode);
		}
		PathRequestManager.Instance.FinishedProcessingPath(waypoints, pathSuccess);
	}

    public Vector2[] RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.Parent;
		}
		Vector2[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
	}

	public Vector2[] SimplifyPath(List<Node> path)
	{
		List<Vector2> waypoints = new List<Vector2>();
		Vector2 directionOld = Vector2.zero;

		for(int i = 1; i < path.Count; i++)
		{
			Vector2 directionNew = new Vector2(path[i - 1].GridX - path[i].GridX, path[i - 1].GridY - path[i].GridY);
			if (directionNew != directionOld)
			{
				waypoints.Add(path[i].WorldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}
}
