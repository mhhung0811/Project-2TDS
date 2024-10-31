using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool Walkable;
    public Vector2 WorldPosition;
	public int GridX;
    public int GridY;
	public Node Parent;
	public int GCost;
	public int HCost;

	public int FCost { get { return GCost + HCost; } }

	public Node(bool walkable, Vector2 worldPosition, int gridX, int gridY)
	{
		Walkable = walkable;
		WorldPosition = worldPosition;
		GridX = gridX;
		GridY = gridY;
	}
}
