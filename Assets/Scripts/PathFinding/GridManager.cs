﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public LayerMask UnwalkableMask;
    public Vector2 GridWorldSize;
    public float NodeRadius;
    public Node[,] Grid;
    public float NodeDiameter;
	public int GridSizeX, GridSizeY;
	public GameObject Player;
	private void Start()
    {
        NodeDiameter = NodeRadius * 2;
        GridSizeX = Mathf.RoundToInt(GridWorldSize.x / NodeDiameter);
		GridSizeY = Mathf.RoundToInt(GridWorldSize.y / NodeDiameter);
        CreateGrid();
	}

    private void CreateGrid()
    {
		Debug.Log("Create Grid");
		Grid = new Node[GridSizeX, GridSizeY];
        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * GridWorldSize.x/2 - Vector2.up * GridWorldSize.y / 2;

        for(int x = 0; x < GridSizeX; x++)
        {
            for(int y = 0; y< GridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * NodeDiameter + NodeRadius) + Vector2.up * (y * NodeDiameter + NodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, NodeRadius, UnwalkableMask));
                Grid[x, y] = new Node(walkable, worldPoint, x, y);
			}
        }
	}

    public Node GetNodeFromWorldPoint(Vector2 worldPosition)
    {
		float percentX = (worldPosition.x - (transform.position.x - GridWorldSize.x / 2)) / GridWorldSize.x;
		float percentY = (worldPosition.y - (transform.position.y - GridWorldSize.y / 2)) / GridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((GridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((GridSizeY - 1) * percentY);
		return Grid[x, y];
	}

	public Node[] GetNeighbors(Node node)
	{
		List<Node> neighbors = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0) // Skip current node
				{
					continue;
				}

				int checkX = node.GridX + x;
				int checkY = node.GridY + y;

				if (checkX >= 0 && checkX < GridSizeX && checkY >= 0 && checkY < GridSizeY)
				{
					neighbors.Add(Grid[checkX, checkY]);
				}
			}
		}
		return neighbors.ToArray();
	}

	public int GetDistance(Node nodeA, Node nodeB)
	{
		int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
		int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

		if (dstX > dstY)
		{
			return 14 * dstY + 10 * (dstX - dstY);
		}
		return 14 * dstX + 10 * (dstY - dstX);
	}

	public List<Node> path;

	private void OnDrawGizmos()
	{
		// Vẽ hình vuông cho lưới
		Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, GridWorldSize.y, 1));

		if (Grid != null)
		{
			Node playerNode = GetNodeFromWorldPoint(Player.transform.position);
			foreach (Node n in Grid)
			{
				Gizmos.color = (n.Walkable) ? new Color(1, 1, 1, 0.5f) : new Color(1, 0, 0, 0.5f);
				if (playerNode == n)
				{
					Gizmos.color = Color.cyan;
				}
				if (path != null)
				{
					if(path.Contains(n))
					{
						Gizmos.color = Color.yellow;
					}
				}
				Gizmos.DrawCube(n.WorldPosition, Vector3.one * (NodeDiameter - 0.1f));
			}
		}
	}

}