using System.Collections;
using System.Collections.Generic;
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
			FindPath(seeker.position, target.position);
		}
	}
	public List<Node> FindPath(Vector2 startPosition, Vector2 targetPosition)
	{
		Node startNode = gridManager.GetNodeFromWorldPoint(startPosition);
		Node targetNode = gridManager.GetNodeFromWorldPoint(targetPosition);

		List<Node> openSet = new List<Node>(); // List of node need to check
		HashSet<Node> closedSet = new HashSet<Node>(); // List of node have checked
		openSet.Add(startNode);

		while(openSet.Count > 0)
		{
			Node currentNode = openSet[0];

			for(int i = 1;i < openSet.Count; i++) // Find node have lowest FCost
			{
				if (openSet[i].FCost < currentNode.FCost || (openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost))
				{
					currentNode = openSet[i];
				}
			}

			openSet.Remove(currentNode);
			closedSet.Add(currentNode);

			if (currentNode == targetNode) // If current node is target node
			{
				return RetracePath(startNode,targetNode);
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
				}
			}
		}

		return null; // No path found
	}

	public List<Node> RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.Parent;
		}

		path.Reverse(); 
		gridManager.path = path; 
		return path; 
	}

}
